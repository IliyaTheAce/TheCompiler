using TheCompiler.Parser.AST;

namespace TheCompiler.Parser;

public class AstPrinter
{
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
            
            case BooleanExpression boolean:
                Console.WriteLine($"{indent}boolean({boolean.Value})");
                break;  
            
            case UnaryExpression unary:
                Console.WriteLine($"{indent}Not");
                Print(unary.Right, indent + "  ");
                break;

            case BinaryExpression bin:
                Console.WriteLine($"{indent}Binary({bin.Operator.Lexeme})");

                Print(bin.Left, indent + "  ");
                Print(bin.Right, indent + "  ");
                break;
        }
    }
}