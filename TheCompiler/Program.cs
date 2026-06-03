using TheCompiler.Lexer;

string source = File.ReadAllText("../../../Samples/Source1.tc");

// Console.WriteLine(source);
var lexer  = new Lexer(source);
var tokens = lexer.GetTokens();

foreach (var token in tokens)
{
    Console.Write(token.ToString() + '\n');
}