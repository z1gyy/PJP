grammar PLC;

// === ENTRY POINT ===
program: statement* EOF;

// === STATEMENTS ===
statement
    : ';'                                                    # emptyStatement
    | type varList ';'                                       # declarationStatement
    | expression ';'                                         # expressionStatement
    | 'read' varList ';'                                     # readStatement
    | 'write' exprList ';'                                   # writeStatement
    | '{' statement* '}'                                     # blockStatement
    | 'if' '(' expression ')' statement ('else' statement)?  # ifStatement
    | 'while' '(' expression ')' statement                   # whileStatement
    ;

// === TYPES ===
type: 'int' | 'float' | 'bool' | 'string';

// === VARIABLE LIST ===
varList: IDENT (',' IDENT)*;

// === EXPRESSION LIST ===
exprList: expression (',' expression)*;

// === EXPRESSIONS ===
// Zohledňuje prioritu a asociativitu
expression
    : IDENT '=' expression                      # assignExpr           // right-associative
    | expression '||' expression                # orExpr
    | expression '&&' expression                # andExpr
    | expression ('==' | '!=') expression       # equalityExpr
    | expression ('<' | '>') expression         # relationalExpr
    | expression ('+' | '-' | '.') expression   # additiveExpr
    | expression ('*' | '/' | '%') expression   # multiplicativeExpr
    | '!' expression                            # notExpr
    | '-' expression                            # unaryMinusExpr
    | '(' expression ')'                        # parenExpr
    | literal                                   # literalExpr
    | IDENT                                     # varExpr
    ;

// === LITERALS ===
literal
    : INT
    | FLOAT
    | BOOL
    | STRING
    ;

// === TOKENS ===
BOOL: 'true' | 'false';

IDENT: [a-zA-Z] [a-zA-Z0-9]*;

INT: [0-9]+;
FLOAT: [0-9]+ '.' [0-9]+;
STRING: '"' (~["\r\n])* '"';

// === IGNOROVANÉ ===
LINE_COMMENT: '//' ~[\r\n]* -> skip;
WS: [ \t\r\n]+ -> skip;
