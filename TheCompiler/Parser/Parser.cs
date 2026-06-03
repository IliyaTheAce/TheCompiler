using TheCompiler.Lexer;
using TheCompiler.Parser.AST;

namespace TheCompiler.Parser;

public class Parser(List<Token> tokens)
{
    private List<Statement> _statements = new();
    private int _current = 0;
    public List<Statement> Parse()
    {
        while (_current < tokens.Count && tokens[_current].Type != TokenType.EndOfFile)
        {
            _statements.Add(ParseStatement());
        }
        
        return _statements;
    }

    private Statement ParseStatement()
    {
        if (Match(TokenType.Let))
            return ParseVariableDeclaration();

        if (Match(TokenType.Print))
            return ParsePrintStatement();
        
        if (Match(TokenType.If))
            return ParseIfStatement();

        throw new Exception("Unknown statement");
    }

    private Statement ParseIfStatement()
    {
        Consume(TokenType.OpenParan);
        Expression condition = ParseExpression();
        Consume(TokenType.CloseParan);
        Consume(TokenType.OpenBracket);
        var blockStatements = new List<Statement>();
        while (!Match(TokenType.CloseBracket))
        {
            blockStatements.Add(ParseStatement());
        }
        return new IfStatement(condition, blockStatements);
    }

    private Statement ParseVariableDeclaration()
    {
        Token name = Consume(TokenType.Identifier);

        Consume(TokenType.Equal);

        Expression value = ParseExpression();

        Consume(TokenType.Semicolon);

        return new VariableDeclaration(
            value,
            name.Lexeme
        );
    }

    private Statement ParsePrintStatement()
    {
        Consume(TokenType.OpenParan);
        Expression exp = ParseExpression();
        Consume(TokenType.CloseParan);
        Consume(TokenType.Semicolon);
        return new PrintStatement(exp);
    }

    private Expression ParseExpression()
    {
        return ParseComparison();
    }

    private Expression ParseComparison()
    {
        Expression expr = ParseTerm();
        while (
            Peek().Type == TokenType.GreaterThan ||
            Peek().Type == TokenType.LessThan ||
            Peek().Type == TokenType.DoubleEquals ||
            Peek().Type == TokenType.NotEquals ||
            Peek().Type == TokenType.GreaterThanEquals ||
            Peek().Type == TokenType.LessThanEquals)
        {
            Token op = Advance();
            Expression right = ParseTerm();
            expr = new BinaryExpression(
                expr,
                op,
                right);
        }
        
        return expr;
    }

    private Expression ParseTerm()
    {
        Expression expr = ParseFactor();

        while (
            Peek().Type == TokenType.Plus ||
            Peek().Type == TokenType.Minus)
        {
            Token op = Advance();

            Expression right = ParseFactor();

            expr = new BinaryExpression(
                expr,
                op,
                right
            );
        }

        return expr;
    }
    
    private Expression ParseFactor()
    {
        Expression expr = ParsePrimary();

        while (
            Peek().Type == TokenType.Star ||
            Peek().Type == TokenType.Slash)
        {
            Token op = Advance();

            Expression right = ParsePrimary();

            expr = new BinaryExpression(
                expr,
                op,
                right
            );
        }

        return expr;
    }
    
    private Expression ParsePrimary()
    {
        if (Match(TokenType.Number))
        {
            Token token = Previous();

            return new NumberExpression(
                int.Parse(token.Lexeme)
            );
        }

        if (Match(TokenType.Identifier))
        {
            Token token = Previous();

            return new IdentifierExpression(
                token.Lexeme
            );
        }

        if (Match(TokenType.OpenParan))
        {
            Expression expr = ParseExpression();

            Consume(TokenType.CloseParan);

            return expr;
        }

        throw new Exception(
            $"Unexpected token {Peek().Type}"
        );
    }
    
    
    private Token Previous()
    {
        return tokens[_current - 1];
    }
    
    private Token Peek()
    {
        return tokens[_current];
    }
    
    private Token Advance()
    {
        return tokens[_current++];
    }

    private bool Match(TokenType type)
    {
        if(Peek().Type != type)
            return false;
        Advance();
        return true;
    }
    
    private Token Consume(TokenType expectedType)
    {
        if (Peek().Type == expectedType)
            return Advance();

        Token current = Peek();

        throw new Exception(
            $"Syntax Error at line {current.Line}, " +
            $"column {current.Column}. " +
            $"Expected {expectedType} but found {current.Type}."
        );
    }
    
    public static void Print(Expression expr, string indent = "")
    {
        switch (expr)
        {
            case NumberExpression number:
                Console.WriteLine($"{indent}Number({number.Value})");
                break;

            case IdentifierExpression id:
                Console.WriteLine($"{indent}Identifier({id.Name})");
                break;

            case BinaryExpression bin:
                Console.WriteLine($"{indent}Binary({bin.Operator.Lexeme})");

                Print(bin.Left, indent + "  ");
                Print(bin.Right, indent + "  ");
                break;
        }
    }
}