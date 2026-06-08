namespace TheCompiler.Parser.AST;

public class FunctionDeclaration(string name,List<Statement> body,List<string> parameters) : Statement
{
    public string Name { get; } = name;
    public List<Statement> Body { get; } = body;
    public List<string> Params = parameters;
}