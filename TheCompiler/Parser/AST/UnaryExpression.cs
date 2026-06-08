using TheCompiler.Lexer;

namespace TheCompiler.Parser.AST;

public class UnaryExpression(Token op, Expression right) : Expression
{
    public Token Operator { get; } = op;
    public Expression Right { get; } = right;
}