namespace TheCompiler.Semantic;

public class FunctionSymbol(string name, int paramsCount)
{
    public string Name = name;
    public int ParamsCount = paramsCount;
}