namespace TheCompiler.Parser.AST;

public class IdentifierExpression(string name) : Expression
{
    public string Name { get; } = name;
}