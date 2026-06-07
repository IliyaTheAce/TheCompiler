using TheCompiler.Lexer;

namespace TheCompiler.Parser.AST;

public class StringExpression(string str) : Expression
{
    public string String { get; } = str;
}