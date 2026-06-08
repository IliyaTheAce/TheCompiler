namespace TheCompiler.Exceptions;

public class ReturnException(object value):Exception
{
    public object Value { get; } = value;
}