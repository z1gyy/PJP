using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using pjpproject;

public class CodeGenVisitor : PLCBaseVisitor<PType>
{
    private readonly Dictionary<string, PType> _symbolTable = new();
    private readonly List<string> _instructions = new();

    public List<string> GetInstructions() => _instructions;

    private static string GetTypeCode(PType type) => type switch
    {
        PType.Int => "I",
        PType.Float => "F",
        PType.Bool => "B",
        PType.String => "S",
        _ => "?"
    };

    private static string GetDefaultValue(PType type) => type switch
    {
        PType.Int => "0",
        PType.Float => "0.0",
        PType.Bool => "false",
        PType.String => "\"\"",
        _ => "?"
    };

    public override PType VisitDeclarationStatement(PLCParser.DeclarationStatementContext context)
    {
        var type = context.type().GetText() switch
        {
            "int" => PType.Int,
            "float" => PType.Float,
            "bool" => PType.Bool,
            "string" => PType.String,
            _ => PType.Error
        };

        foreach (var id in context.varList().IDENT())
        {
            var name = id.GetText();
            _symbolTable[name] = type;
            _instructions.Add($"push {GetTypeCode(type)} {GetDefaultValue(type)}");
            _instructions.Add($"save {name}");
        }

        return PType.Error;
    }

    public override PType VisitUnaryMinusExpr(PLCParser.UnaryMinusExprContext context)
    {
        var type = Visit(context.expression());
        if (type == PType.Int || type == PType.Float)
        {
            _instructions.Add($"uminus {GetTypeCode(type)}");
            return type;
        }
        return PType.Error;
    }

    public override PType VisitLiteralExpr(PLCParser.LiteralExprContext context)
    {
        var text = context.literal().GetText();
        if (context.literal().INT() != null) {
            _instructions.Add($"push I {text}");
            return PType.Int;
        }
        if (context.literal().FLOAT() != null) {
            _instructions.Add($"push F {text}");
            return PType.Float;
        }
        if (context.literal().STRING() != null) {
            _instructions.Add($"push S {text}");
            return PType.String;
        }
        if (text == "true" || text == "false") {
            _instructions.Add($"push B {text}");
            return PType.Bool;
        }
        return PType.Error;
    }

    public override PType VisitVarExpr(PLCParser.VarExprContext context)
    {
        var name = context.IDENT().GetText();
        if (_symbolTable.TryGetValue(name, out var type)) {
            _instructions.Add($"load {name}");
            return type;
        }
        return PType.Error;
    }

    public override PType VisitAssignExpr(PLCParser.AssignExprContext context)
    {
        var name = context.IDENT().GetText();
        var rightType = Visit(context.expression());
        var leftType = _symbolTable.ContainsKey(name) ? _symbolTable[name] : rightType;

        if (leftType == PType.Float && rightType == PType.Int)
            InsertAfterLastIntPush("itof");

        _instructions.Add($"save {name}");
        _instructions.Add($"load {name}");

        if (context.Parent is not PLCParser.AssignExprContext)
            _instructions.Add("pop");

        _symbolTable[name] = leftType;
        return leftType;
    }

    public override PType VisitAdditiveExpr(PLCParser.AdditiveExprContext context)
    {
        var leftType = Visit(context.expression(0));
        var rightType = Visit(context.expression(1));
        var op = context.GetChild(1).GetText();

        if (op == ".")
        {
            if (leftType == PType.String || rightType == PType.String)
            {
                _instructions.Add("concat");
                return PType.String;
            }
        }
        else if ((leftType == PType.Int || leftType == PType.Float) &&
                 (rightType == PType.Int || rightType == PType.Float))
        {
            if (leftType == PType.Float && rightType == PType.Int)
                _instructions.Add("itof");
            else if (leftType == PType.Int && rightType == PType.Float)
                InsertAfterLastIntPush("itof");

            string instruction = op switch
            {
                "+" => $"add {GetTypeCode(PromoteType(leftType, rightType))}",
                "-" => $"sub {GetTypeCode(PromoteType(leftType, rightType))}",
                "*" => $"mul {GetTypeCode(PromoteType(leftType, rightType))}",
                "/" => $"div {GetTypeCode(PromoteType(leftType, rightType))}",
                "%" => $"mod {GetTypeCode(PromoteType(leftType, rightType))}",
                _ => "ERROR"
            };

            _instructions.Add(instruction);
            return PromoteType(leftType, rightType);
        }

        return PType.Error;
    }

    public override PType VisitMultiplicativeExpr(PLCParser.MultiplicativeExprContext context)
    {
        var leftType = Visit(context.expression(0));
        var rightType = Visit(context.expression(1));
        var op = context.GetChild(1).GetText();

        if ((leftType == PType.Int || leftType == PType.Float) &&
            (rightType == PType.Int || rightType == PType.Float))
        {
            if (leftType == PType.Float && rightType == PType.Int)
                _instructions.Add("itof");
            else if (leftType == PType.Int && rightType == PType.Float)
                InsertAfterLastIntPush("itof");

            _instructions.Add(op switch
            {
                "*" => $"mul {GetTypeCode(PromoteType(leftType, rightType))}",
                "/" => $"div {GetTypeCode(PromoteType(leftType, rightType))}",
                "%" => "mod",
                _ => "ERROR"
            });

            return PromoteType(leftType, rightType);
        }

        return PType.Error;
    }

    public override PType VisitRelationalExpr(PLCParser.RelationalExprContext context)
    {
        var rightType = Visit(context.expression(1));
        var leftType = Visit(context.expression(0));
        var op = context.GetChild(1).GetText();

        if (leftType == PType.Int && rightType == PType.Float)
            InsertAfterLastIntPush("itof");

        if (leftType == PType.Float && rightType == PType.Int)
            _instructions.Add("itof");

        var typeCode = GetTypeCode(PromoteType(leftType, rightType));
        _instructions.Add(op switch
        {
            "<" => $"lt {typeCode}",
            ">" => $"gt {typeCode}",
            _ => "ERROR"
        });

        return PType.Bool;
    }

    public override PType VisitEqualityExpr(PLCParser.EqualityExprContext context)
    {
        var rightType = Visit(context.expression(1));
        var leftType = Visit(context.expression(0));
        var op = context.GetChild(1).GetText();

        if (leftType == PType.Int && rightType == PType.Float)
            InsertAfterLastIntPush("itof");

        if (leftType == PType.Float && rightType == PType.Int)
            _instructions.Add("itof");

        var typeCode = GetTypeCode(PromoteType(leftType, rightType));
        _instructions.Add($"eq {typeCode}");

        if (op == "!=")
            _instructions.Add("not");

        return PType.Bool;
    }

    public override PType VisitOrExpr(PLCParser.OrExprContext context)
    {
        Visit(context.expression(0));
        Visit(context.expression(1));
        _instructions.Add("or");
        return PType.Bool;
    }

    public override PType VisitAndExpr(PLCParser.AndExprContext context)
    {
        Visit(context.expression(0));
        Visit(context.expression(1));
        _instructions.Add("and");
        return PType.Bool;
    }

    public override PType VisitNotExpr(PLCParser.NotExprContext context)
    {
        Visit(context.expression());
        _instructions.Add("not");
        return PType.Bool;
    }

    public override PType VisitParenExpr(PLCParser.ParenExprContext context)
    {
        return Visit(context.expression());
    }

    public override PType VisitNovy(PLCParser.NovyContext context)
    {
        var leftType = Visit(context.expression(0));
        var rightType = Visit(context.expression(1));

        if (leftType == PType.Int && rightType == PType.Int)
        {
            _instructions.Add("shl");
            return PType.Int;
        }
        return PType.Error;
    }

    public override PType VisitWriteStatement(PLCParser.WriteStatementContext context)
    {
        int exprCount = 0;
        foreach (var expr in context.exprList().expression())
        {
            Visit(expr);
            exprCount++;
        }
        _instructions.Add($"print {exprCount}");
        return PType.Error;
    }

    public override PType VisitReadStatement(PLCParser.ReadStatementContext context)
    {
        foreach (var id in context.varList().IDENT())
        {
            var name = id.GetText();
            if (_symbolTable.TryGetValue(name, out var type))
            {
                _instructions.Add($"read {GetTypeCode(type)}");
                _instructions.Add($"save {name}");
            }
        }
        return PType.Error;
    }

    public override PType VisitBlockStatement(PLCParser.BlockStatementContext context)
    {
        foreach (var stmt in context.statement())
            Visit(stmt);
        return PType.Error;
    }

    private int _labelCounter = 0;

    public override PType VisitIfStatement(PLCParser.IfStatementContext context)
    {
        int elseLabel = _labelCounter++;
        int endLabel = _labelCounter++;

        Visit(context.expression());
        _instructions.Add($"fjmp {elseLabel}");

        Visit(context.statement(0));

        if (context.statement().Length > 1)
        {
            _instructions.Add($"jmp {endLabel}");
            _instructions.Add($"label {elseLabel}");
            Visit(context.statement(1));
        }
        else
        {
            _instructions.Add($"label {elseLabel}");
        }

        _instructions.Add($"label {endLabel}");
        return PType.Error;
    }

    public override PType VisitWhileStatement(PLCParser.WhileStatementContext context)
    {
        int startLabel = _labelCounter++;
        int endLabel = _labelCounter++;

        _instructions.Add($"label {startLabel}");
        Visit(context.expression());
        _instructions.Add($"fjmp {endLabel}");
        Visit(context.statement());
        _instructions.Add($"jmp {startLabel}");
        _instructions.Add($"label {endLabel}");

        return PType.Error;
    }

    private static PType PromoteType(PType a, PType b)
    {
        return (a == PType.Float || b == PType.Float) ? PType.Float : PType.Int;
    }

    private void InsertAfterLastIntPush(string instruction)
    {
        for (int i = _instructions.Count - 1; i >= 0; i--)
        {
            if (_instructions[i].StartsWith("push I"))
            {
                _instructions.Insert(i + 1, instruction);
                return;
            }
        }
        _instructions.Add(instruction);
    }
}
