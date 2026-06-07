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
    
    DoubleQuote,
    
    Semicolon,
    
    If,
    Else,
    While,
    
    DoubleEquals,
    NotEquals,
    Not,
    GreaterThan,
    LessThan,
    GreaterThanEquals,
    LessThanEquals,
    
    And,
    Or,
    
    OpenBracket,
    CloseBracket,
    
    True,
    False,
    
    EndOfFile,
    String
}