namespace TheCompiler.Parser.AST;

public class NumberExpression(int value) : Expression
{
    public int Value { get; } = value;
}