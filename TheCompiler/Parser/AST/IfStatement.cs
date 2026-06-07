using TheCompiler.Lexer;

namespace TheCompiler.Parser.AST;

public class IfStatement(Expression condition, List<Statement> block, List<Statement>? elseBlock)
    : Statement
{
    public Expression Condition { get; } = condition;
    public readonly List<Statement> Block = block;
    public readonly List<Statement>? ElseBlock = elseBlock;
}