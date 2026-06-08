using TheCompiler.Parser.AST;

namespace TheCompiler.Semantic;

public class SemanticAnalyzer
{
    private Dictionary<string, VariableSymbol> _variables = new();
    private Dictionary<string, FunctionSymbol> _functions = new();

    public void Analyze(List<Statement> statements)
    {
        foreach (var statement in statements)
        {
            Visit(statement);
        }
    }
    
    private void Visit(Statement statement)
    {
        switch (statement)
        {
            case ExpressionStatement s:   Visit(s); break;
            case FunctionDeclaration s:   Visit(s); break;
            case IfStatement s:           Visit(s); break;
            case PrintStatement s:        Visit(s); break;
            case ReturnStatement s:       Visit(s); break;
            case VariableAssignment s:    Visit(s); break;
            case VariableDeclaration s:   Visit(s); break;
            case WhileStatement s:        Visit(s); break;
            default: throw new Exception($"Unknown statement type: {statement.GetType().Name}");
        }
    }

    private void Visit(VariableDeclaration statement)
    {
        if (_variables.ContainsKey(statement.Name))
            throw new Exception($"Variable {statement.Name} already exists");

        _variables[statement.Name] = new VariableSymbol(statement.Name);
    }
    
    private void Visit(VariableAssignment statement)
    {
        if (!_variables.ContainsKey(statement.Name))
            throw new Exception($"Undefined variable {statement.Name}");
        
        VisitExpression(statement.Value);
    }
    
    private void Visit(FunctionDeclaration statement)
    {
        if (_functions.ContainsKey(statement.Name))
            throw new Exception($"Function {statement.Name} already defined!");

        _functions[statement.Name] = new FunctionSymbol(statement.Name,statement.Params.Count);
    }
    
    void Visit(ExpressionStatement statement)
    {
        VisitExpression(statement.Expression);
    }  
    
    void Visit(IfStatement statement)
    {
        VisitExpression(statement.Condition);
        foreach (var blockStatement in statement.Block)
        {
            Visit(blockStatement);
        }
        
        foreach (var elseStatement in statement.ElseBlock)
        {
            Visit(elseStatement);
        }
    }  
    
    void Visit(PrintStatement statement)
    {
        VisitExpression(statement.Exp);
    } 
    
    void Visit(ReturnStatement statement)
    {
        VisitExpression(statement.Expression);
    }
    
    void Visit(WhileStatement statement)
    {
        VisitExpression(statement.Condition);
        foreach (var blockStatement in statement.Block)
        {
            Visit(blockStatement);
        } 
    }

    private void VisitExpression(Expression expr)
    {
        switch (expr)
        {
            case BinaryExpression b:
                VisitExpression(b);
                break;
            case IdentifierExpression i:
                VisitExpression(i);
                break;
            case FunctionCallExpression f:
                VisitExpression(f);
                break;
            
            default:
                break;
        }
    }

    void VisitExpression(IdentifierExpression expr)
    {
        if (!_variables.ContainsKey(expr.Name))
            throw new Exception($"Undefined variable: {expr.Name}");
    }

    private void VisitExpression(FunctionCallExpression expr)
    {
        if (!_functions.ContainsKey(expr.Name))
            throw new Exception($"Undefined function: {expr.Name}");

        var fn = _functions[expr.Name];

        if (expr.Arguments.Count != fn.ParamsCount)
            throw new Exception($"Function {expr.Name} expects {fn.ParamsCount} args. But  got {expr.Arguments.Count} args");
    }

    private void VisitExpression(BinaryExpression expr)
    {
        VisitExpression(expr.Left);
        VisitExpression(expr.Right);
    }
}