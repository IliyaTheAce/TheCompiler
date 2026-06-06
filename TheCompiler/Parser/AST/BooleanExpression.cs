using TheCompiler.Lexer;

namespace TheCompiler.Parser.AST;

public class BooleanExpression(bool value) : Expression
{
    public bool Value { get; } = value;
}