namespace TheCompiler.Lexer;

public class Lexer(string source)
{
    private int _position = 0;
    private int _line = 1;
    private int _column = 1;
    private string _currentLexeme = "";
    
    private List<Token> _tokens = new();
    
    public List<Token> GetTokens()
    {
        while (_position < source.Length)
        {
            char current = source[_position];
            var startPosition = _column;
            
            if (char.IsWhiteSpace(current))
            {
                Advance();
                continue;
            }

            if (char.IsNumber(current))
            {
                var start = _position;
                while (_position < source.Length && char.IsNumber(source[_position]))
                {
                    Advance();
                }
                var number = source.Substring(start, _position - start);
                _tokens.Add(new Token(TokenType.Number, number,_line,startPosition));
                
                continue;
            }
            
            if (char.IsLetter(current))
            {
                var start = _position;
                while (_position < source.Length && char.IsLetterOrDigit(source[_position]))
                {
                    Advance();
                }
                var text = source.Substring(start, _position - start);

                var type = text switch
                {
                    "let" => TokenType.Let,
                    "print" => TokenType.Print,
                    _ => TokenType.Identifier
                };
                _tokens.Add(new Token(type, text,_line,startPosition));
                
                continue;
            }
            
            switch(current)
            {
                case '+':
                    _tokens.Add(new Token(TokenType.Plus, "+",_line,startPosition));
                    break;

                case '=':
                    _tokens.Add(new Token(TokenType.Equal, "=",_line,startPosition));
                    break;

                case ';':
                    _tokens.Add(new Token(TokenType.Semicolon, ";",_line,startPosition));
                    break;
                
                case '(':
                    _tokens.Add(new Token(TokenType.OpenParan, "(",_line,startPosition));
                    break;
                
                case ')':
                    _tokens.Add(new Token(TokenType.CloseParan, ")",_line,startPosition));
                    break;
            }
            
            Advance();
        }

        return _tokens;
    }
    
    private char Advance()
    {
        var current = source[_position];

        _position++;

        if (current == '\n')
        {
            _line++;
            _column = 1;
        }
        else if (current != '\r')
        {
            _column++;
        }

        return current;
    }
}