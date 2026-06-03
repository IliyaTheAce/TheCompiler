namespace TheCompiler.Lexer;

public class Token(TokenType type, string lexeme,int line,int column)
{
    public readonly TokenType Type = type;
    public readonly string Lexeme = lexeme;
    public readonly int Line = line;
    public readonly int Column = column;

    public override string ToString()
    {
        return Lexeme + ": " + Type +  " (" + Line + "," + Column + ")" ;
    }
}