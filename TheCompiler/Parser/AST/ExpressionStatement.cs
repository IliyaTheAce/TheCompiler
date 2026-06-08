namespace TheCompiler.Parser.AST;

public class ExpressionStatement(Expression exp):Statement
{
    public Expression Expression { get; } = exp;
}