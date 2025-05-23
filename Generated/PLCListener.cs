//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from c:/Users/Žigy-san/Downloads/pjp-main (3)/pjp-main/PLC.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="PLCParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface IPLCListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="PLCParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] PLCParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLCParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] PLCParser.ProgramContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>emptyStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEmptyStatement([NotNull] PLCParser.EmptyStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>emptyStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEmptyStatement([NotNull] PLCParser.EmptyStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>declarationStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDeclarationStatement([NotNull] PLCParser.DeclarationStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>declarationStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDeclarationStatement([NotNull] PLCParser.DeclarationStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>expressionStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpressionStatement([NotNull] PLCParser.ExpressionStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expressionStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpressionStatement([NotNull] PLCParser.ExpressionStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>readStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReadStatement([NotNull] PLCParser.ReadStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>readStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReadStatement([NotNull] PLCParser.ReadStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>writeStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWriteStatement([NotNull] PLCParser.WriteStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>writeStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWriteStatement([NotNull] PLCParser.WriteStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>blockStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockStatement([NotNull] PLCParser.BlockStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>blockStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockStatement([NotNull] PLCParser.BlockStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ifStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStatement([NotNull] PLCParser.IfStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ifStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStatement([NotNull] PLCParser.IfStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>whileStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhileStatement([NotNull] PLCParser.WhileStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>whileStatement</c>
	/// labeled alternative in <see cref="PLCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhileStatement([NotNull] PLCParser.WhileStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PLCParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] PLCParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLCParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] PLCParser.TypeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PLCParser.varList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarList([NotNull] PLCParser.VarListContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLCParser.varList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarList([NotNull] PLCParser.VarListContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PLCParser.exprList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExprList([NotNull] PLCParser.ExprListContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLCParser.exprList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExprList([NotNull] PLCParser.ExprListContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>orExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOrExpr([NotNull] PLCParser.OrExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>orExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOrExpr([NotNull] PLCParser.OrExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>additiveExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAdditiveExpr([NotNull] PLCParser.AdditiveExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>additiveExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAdditiveExpr([NotNull] PLCParser.AdditiveExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>relationalExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRelationalExpr([NotNull] PLCParser.RelationalExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>relationalExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRelationalExpr([NotNull] PLCParser.RelationalExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>parenExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenExpr([NotNull] PLCParser.ParenExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>parenExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenExpr([NotNull] PLCParser.ParenExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>varExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarExpr([NotNull] PLCParser.VarExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>varExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarExpr([NotNull] PLCParser.VarExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>notExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNotExpr([NotNull] PLCParser.NotExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>notExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNotExpr([NotNull] PLCParser.NotExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>unaryMinusExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnaryMinusExpr([NotNull] PLCParser.UnaryMinusExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>unaryMinusExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnaryMinusExpr([NotNull] PLCParser.UnaryMinusExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>literalExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteralExpr([NotNull] PLCParser.LiteralExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>literalExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteralExpr([NotNull] PLCParser.LiteralExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>novy</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNovy([NotNull] PLCParser.NovyContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>novy</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNovy([NotNull] PLCParser.NovyContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>assignExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignExpr([NotNull] PLCParser.AssignExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>assignExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignExpr([NotNull] PLCParser.AssignExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>multiplicativeExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplicativeExpr([NotNull] PLCParser.MultiplicativeExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>multiplicativeExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplicativeExpr([NotNull] PLCParser.MultiplicativeExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>equalityExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEqualityExpr([NotNull] PLCParser.EqualityExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>equalityExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEqualityExpr([NotNull] PLCParser.EqualityExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>andExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAndExpr([NotNull] PLCParser.AndExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>andExpr</c>
	/// labeled alternative in <see cref="PLCParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAndExpr([NotNull] PLCParser.AndExprContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PLCParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] PLCParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PLCParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] PLCParser.LiteralContext context);
}
