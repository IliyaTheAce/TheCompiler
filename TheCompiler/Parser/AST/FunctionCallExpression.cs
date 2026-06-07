namespace TheCompiler.Parser.AST;

public class FunctionCallStatement(string name) : Statement
{
    public string Name { get; } = name;
}