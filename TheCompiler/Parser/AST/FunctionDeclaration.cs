namespace TheCompiler.Parser.AST;

public class FunctionDeclaration(string name,List<Statement> body) : Statement
{
    public string Name { get; } = name;
    public List<Statement> Body { get; } = body;
}