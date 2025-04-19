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
    : '!' expression                                       # notExpr    
    | '-' expression                                       # unaryMinusExpr
    | <assoc=left> expression ('*' | '/' | '%') expression # multiplicativeExpr   
    | <assoc=left> expression ('+' | '-' | '.') expression # additiveExpr
    | <assoc=left> expression ('<' | '>') expression       # relationalExpr
    | <assoc=left> expression ('==' | '!=') expression     # equalityExpr
    | <assoc=left> expression '&&' expression              # andExpr
    | <assoc=left> expression '||' expression              # orExpr
    | IDENT '=' expression                                # assignExpr
    | '(' expression ')'                                  # parenExpr
    | expression '<<' expression                          # novy
    | literal                                             # literalExpr
    | IDENT                                               # varExpr
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