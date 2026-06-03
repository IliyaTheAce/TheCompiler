namespace TheCompiler.Lexer;

public enum TokenType
{
    Identifier,
    Let,
    
    Print,
    
    Number,
    
    Plus,
    Minus,
    Star,
    Slash,
    
    Equal,
    
    OpenParan,
    CloseParan,
    
    Semicolon,
    
    If,
    
    DoubleEquals,
    NotEquals,
    Not,
    GreaterThan,
    LessThan,
    GreaterThanEquals,
    LessThanEquals,
    
    OpenBracket,
    CloseBracket,
    
    EndOfFile
}