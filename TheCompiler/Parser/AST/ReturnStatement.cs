namespace TheCompiler.Parser.AST;

public class ReturnStatement(Expression exp):Statement
{
    public Expression Expression { get; } = exp;
}