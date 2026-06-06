using TheCompiler.Lexer;

namespace TheCompiler.Parser.AST;

public class PrintStatement(Expression expression) : Statement
{
    public Expression Exp { get; } = expression;
}