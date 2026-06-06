namespace TheCompiler.Parser.AST;

public class VariableAssignment(Expression value, string name) : Statement
{
    public Expression Value { get; } = value;
    public string Name { get; } = name;
}