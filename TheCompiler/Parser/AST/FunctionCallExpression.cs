namespace TheCompiler.Parser.AST;

public class FunctionCallExpression(string name,List<Expression> arguments) : Expression
{
    public string Name { get; } = name;
    public List<Expression> Arguments = arguments;
}