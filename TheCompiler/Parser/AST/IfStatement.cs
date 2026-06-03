using TheCompiler.Lexer;

namespace TheCompiler.Parser.AST;

public class IfStatement(Expression condition,List<Statement> block) : Statement
{
    public Expression Condition { get; } = condition;
    public List<Statement> Block = block;
}