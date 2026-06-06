using TheCompiler.Lexer;
using TheCompiler.Parser.AST;

namespace TheCompiler.Interprator;

public class Interpreter
{
    private readonly Dictionary<string, object> _variables = new();

    public void Execute(List<Statement> statements)
    {
        foreach (var statement in statements)
        {
            ExecuteStatement(statement);
        }
    }
    
    private void ExecuteStatement(Statement statement)
    {
        switch (statement)
        {
            case VariableDeclaration variable:
                ExecuteVariableDeclaration(variable);
                break;     
            
            case VariableAssignment variable:
                ExecuteVariableAssignment(variable);
                break;

            case PrintStatement print:
                ExecutePrint(print);
                break;
            
            case IfStatement ifStmt:
                ExecuteIfStatement(ifStmt);
                break;
            
            case WhileStatement whileStmt:
                ExecuteWhileStatement(whileStmt);
                break;
        }
    }

    private void ExecuteVariableAssignment(VariableAssignment variable)
    {
        object value = Evaluate(variable.Value);

        _variables[variable.Name] = value;
    }

    private void ExecuteVariableDeclaration(
        VariableDeclaration variable)
    {
        object value = Evaluate(variable.Value);

        _variables[variable.Name] = value;
    }
    
    private void ExecutePrint(PrintStatement print)
    {
        object value = Evaluate(print.Exp);

        Console.WriteLine(value);
    }
    
    private void ExecuteIfStatement(
        IfStatement statement)
    {
        bool condition =
            Convert.ToBoolean(
                Evaluate(statement.Condition));

        if (condition)
        {
            foreach (var bodyStatement in statement.Block)
            {
                ExecuteStatement(bodyStatement);
            }
        }
    }
    
    private void ExecuteWhileStatement(
        WhileStatement statement)
    {
        while (Convert.ToBoolean(
                   Evaluate(statement.Condition)))
        {
            foreach (var bodyStatement in statement.Block)
            {
                ExecuteStatement(bodyStatement);
            }
        }
    }
    
    private object Evaluate(Expression expression)
    {
        switch (expression)
        {
            case NumberExpression number:
                return number.Value;

            case IdentifierExpression identifier:
                return _variables[identifier.Name];

            case BinaryExpression binary:
                return EvaluateBinary(binary);

            default:
                throw new Exception(
                    $"Unknown expression type {expression.GetType()}"
                );
        }
    }
    
    private object EvaluateBinary(BinaryExpression binary)
    {
        int left = Convert.ToInt32(
            Evaluate(binary.Left));

        int right = Convert.ToInt32(
            Evaluate(binary.Right));

        switch (binary.Operator.Type)
        {
            case TokenType.Plus:
                return left + right;

            case TokenType.Minus:
                return left - right;

            case TokenType.Star:
                return left * right;

            case TokenType.Slash:
                return left / right;

            case TokenType.DoubleEquals:
                return Equals(left, right);

            case TokenType.NotEquals:
                return !Equals(left, right);

            case TokenType.GreaterThan:
                return left > right;

            case TokenType.LessThan:
                return left < right;
            
            default:
                throw new Exception(
                    $"Unsupported operator {binary.Operator.Type}"
                );
        }
    }
}