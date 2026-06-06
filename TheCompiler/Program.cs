using TheCompiler.Interprator;
using TheCompiler.Lexer;
using TheCompiler.Parser;
using TheCompiler.Parser.AST;

string source = File.ReadAllText("../../../Samples/Source6.tc");

var lexer  = new Lexer(source);
var tokens = lexer.GetTokens();

Console.WriteLine($" ----- Tokens: {tokens.Count} ----- \n");
foreach (var token in tokens)
{
    Console.Write(token.ToString() + '\n');
}

var parser = new Parser(tokens);
var statements  = parser.Parse();

Console.WriteLine($" ----- Parsed Nodes: {statements.Count} ----- \n");
foreach (var node in statements)
{
    Print(node);
}

Console.WriteLine($" ----- Output ----- \n");

Interpreter interpreter = new();
interpreter.Execute(statements);

void Print(Statement stmt, string indent = "")
{
    switch (stmt)
    {
        case VariableDeclaration variable:
            Console.WriteLine(
                $"{indent}VariableDeclaration({variable.Name})");

            AstPrinter.Print(variable.Value, indent + "  ");
            break;

        case PrintStatement print:
            Console.WriteLine($"{indent}PrintStatement");

            AstPrinter.Print(print.Exp, indent + "  ");
            break;  
        
        case IfStatement If:
            Console.WriteLine($"{indent}IfStatement");
            Console.WriteLine($"{indent}Condition:");
            AstPrinter.Print(If.Condition, indent + "   ");
            Console.WriteLine($"{indent}Body:");
            foreach (Statement blockStatement in If.Block)
            { 
                Print(blockStatement , "   ");
            }
            break;  
        case WhileStatement While:
            Console.WriteLine($"{indent}WhileStatement");
            Console.WriteLine($"{indent}Condition:");
            AstPrinter.Print(While.Condition, indent + "   ");
            Console.WriteLine($"{indent}Body:");
            foreach (Statement blockStatement in While.Block)
            { 
                Print(blockStatement , "   ");
            }
            break;   
        
        case VariableAssignment assignment:
            Console.WriteLine($"{indent}VariableAssignment({assignment.Name})");
            AstPrinter.Print(assignment.Value, indent + "   ");
            break;
    }
}