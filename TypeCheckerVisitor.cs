using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;

namespace pjpproject
{
    public enum PType { Int, Float, Bool, String, Error }

    public class TypeCheckerVisitor : PLCBaseVisitor<PType>
    {
        private readonly Dictionary<string, PType> _symbolTable = new();

        public override PType VisitDeclarationStatement(PLCParser.DeclarationStatementContext context)
        {
            var declaredType = GetTypeFromContext(context.type());
            foreach (var id in context.varList().IDENT())
            {
                string varName = id.GetText();
                if (_symbolTable.ContainsKey(varName))
                {
                    Errors.ReportError(id.Symbol, $"Variable '{varName}' is already declared.");
                }
                else
                {
                    _symbolTable[varName] = declaredType;
                }
            }
            return PType.Error;
        }

        public override PType VisitVarExpr(PLCParser.VarExprContext context)
        {
            string varName = context.IDENT().GetText();
            if (!_symbolTable.TryGetValue(varName, out var type))
            {
                Errors.ReportError(context.IDENT().Symbol, $"Variable '{varName}' is not declared.");
                return PType.Error;
            }
            return type;
        }

        public override PType VisitLiteralExpr(PLCParser.LiteralExprContext context)
        {
            var lit = context.literal();
            if (lit.INT() != null) return PType.Int;
            if (lit.FLOAT() != null) return PType.Float;
            if (lit.STRING() != null) return PType.String;
            if (lit.BOOL() != null) return PType.Bool;
            return PType.Error;
        }

        public override PType VisitAssignExpr(PLCParser.AssignExprContext context)
        {
            var left = context.IDENT();
            var right = context.expression();

            string varName = left.GetText();
            if (!_symbolTable.TryGetValue(varName, out var varType))
            {
                Errors.ReportError(left.Symbol, $"Variable '{varName}' is not declared.");
                return PType.Error;
            }

            var rightType = Visit(right);
            if (!IsAssignable(varType, rightType))
            {
                Errors.ReportError(context.Start, $"Cannot assign {rightType} to variable of type {varType}.");
                return PType.Error;
            }

            return varType;
        }

        public override PType VisitAdditiveExpr(PLCParser.AdditiveExprContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            var op = context.GetChild(1).GetText();

            if (op == "<<")
            {
                return PType.String;   
            }
            
            if (op == ".")
            {
                if (left == PType.String && right == PType.String) return PType.String;
            }
            else if (AreBothNumeric(left, right))
            {
                return PromoteType(left, right);
            }

            Errors.ReportError(context.Start, $"Invalid operands for '{op}': {left}, {right}");
            return PType.Error;
        }

        public override PType VisitMultiplicativeExpr(PLCParser.MultiplicativeExprContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            var op = context.GetChild(1).GetText();

            if (op == "%" && left == PType.Int && right == PType.Int)
                return PType.Int;
            if (AreBothNumeric(left, right)  && op != "%")
                return PromoteType(left, right);

            Errors.ReportError(context.Start, $"Invalid operands for '{op}': {left}, {right}");
            return PType.Error;
        }

        public override PType VisitRelationalExpr(PLCParser.RelationalExprContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            if (AreBothNumeric(left, right))
                return PType.Bool;

            Errors.ReportError(context.Start, "Relational operators require numeric operands.");
            return PType.Error;
        }

        public override PType VisitEqualityExpr(PLCParser.EqualityExprContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            if (left == right || AreBothNumeric(left, right))
                return PType.Bool;

            Errors.ReportError(context.Start, $"Cannot compare {left} and {right}.");
            return PType.Error;
        }

        public override PType VisitOrExpr(PLCParser.OrExprContext context)
        {
            return CheckBoolBinary(context, "||");
        }

        public override PType VisitAndExpr(PLCParser.AndExprContext context)
        {
            return CheckBoolBinary(context, "&&");
        }

        public override PType VisitNotExpr(PLCParser.NotExprContext context)
        {
            var operand = Visit(context.expression());
            if (operand != PType.Bool)
                Errors.ReportError(context.Start, "Operator '!' requires a boolean operand.");
            return PType.Bool;
        }

        public override PType VisitUnaryMinusExpr(PLCParser.UnaryMinusExprContext context)
        {
            var operand = Visit(context.expression());
            if (operand == PType.Int || operand == PType.Float)
                return operand;

            Errors.ReportError(context.Start, "Unary minus requires numeric operand.");
            return PType.Error;
        }

        public override PType VisitIfStatement(PLCParser.IfStatementContext context)
        {
            var condType = Visit(context.expression());
            if (condType != PType.Bool)
                Errors.ReportError(context.expression().Start, "Condition in 'if' must be boolean.");

            Visit(context.statement(0));
            if (context.statement().Length > 1)
                Visit(context.statement(1));

            return PType.Error;
        }

        public override PType VisitWhileStatement(PLCParser.WhileStatementContext context)
        {
            var condType = Visit(context.expression());
            if (condType != PType.Bool)
                Errors.ReportError(context.expression().Start, "Condition in 'while' must be boolean.");

            Visit(context.statement());
            return PType.Error;
        }

        public override PType VisitBlockStatement(PLCParser.BlockStatementContext context)
        {
            foreach (var stmt in context.statement())
                Visit(stmt);
            return PType.Error;
        }

        public override PType VisitWriteStatement(PLCParser.WriteStatementContext context)
        {
            foreach (var expr in context.exprList().expression())
                Visit(expr);
            return PType.Error;
        }

        public override PType VisitReadStatement(PLCParser.ReadStatementContext context)
        {
            foreach (var id in context.varList().IDENT())
            {
                string name = id.GetText();
                if (!_symbolTable.ContainsKey(name))
                    Errors.ReportError(id.Symbol, $"Variable '{name}' is not declared.");
            }
            return PType.Error;
        }

        // === Pomocné metody ===

        private PType GetTypeFromContext(PLCParser.TypeContext ctx)
        {
            return ctx.GetText() switch
            {
                "int" => PType.Int,
                "float" => PType.Float,
                "bool" => PType.Bool,
                "string" => PType.String,
                _ => PType.Error,
            };
        }

        private static bool AreBothNumeric(PType a, PType b) =>
            (a == PType.Int || a == PType.Float) && (b == PType.Int || b == PType.Float);

        private static PType PromoteType(PType a, PType b) =>
            (a == PType.Float || b == PType.Float) ? PType.Float : PType.Int;

        private static bool IsAssignable(PType target, PType value)
        {
            if (target == value) return true;
            if (target == PType.Float && value == PType.Int) return true;
            return false;
        }

        private PType CheckBoolBinary(ParserRuleContext context, string op)
        {
            var left = Visit(context.GetRuleContext<PLCParser.ExpressionContext>(0));
            var right = Visit(context.GetRuleContext<PLCParser.ExpressionContext>(1));
            if (left == PType.Bool && right == PType.Bool)
                return PType.Bool;

            Errors.ReportError(context.Start, $"Operator '{op}' requires boolean operands.");
            return PType.Error;
        }

        public override PType VisitParenExpr(PLCParser.ParenExprContext context)
        {
            return Visit(context.expression());
        }
/*
        public override PType VisitAdditiveExpr(PLCParser.AdditiveExprContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            var op = context.GetChild(1).GetText();

            if (op == "<<")
            {
                return PType.String;   
            }

            if (op == ".")
            {
                return PType.String;
            }

            if (AreBothNumeric(left, right))
            {
                return PromoteType(left, right);
            }

            Errors.ReportError(context.Start, $"Invalid operands for '{op}': {left}, {right}");
            return PType.Error;
        }
*/

    }
}