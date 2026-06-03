using TheCompiler.Lexer;

namespace TheCompiler.Parser.AST;

public class BinaryExpression(Expression left,Token op, Expression right) : Expression
{
    public Expression Left { get; } = left;
    public Token Operator { get; } = op;
    public Expression Right { get; } = right;
}