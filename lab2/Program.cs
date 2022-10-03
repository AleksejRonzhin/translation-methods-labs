using lab2;
using lab2.exceptions;
using lab2.parameters;
using lab2.tokens;
using library.exceptions;

try
{
    var inputFilename = new InputFilenameParameter(TakeArgOrThrow(0, "файл исходного выражения")).GetValue();
    var tokensFilename = new TokensFilenameParameter(TakeArgOrThrow(1, "файл токенов")).GetValue();
    var symbolsTableFilename = new SymbolsTableFilenameParameter(TakeArgOrThrow(2, "файл таблицы символов")).GetValue();

    using (TextReader inputTextReader = new StreamReader(inputFilename))
    {
        (List<Token> tokens, SymbolsTable table) = new LexicalAnalyzer().Analyze(inputTextReader);
        WriteTokens(tokensFilename, tokens);
        WriteSymbolsTable(symbolsTableFilename, table);

    }
}
# region Обработка ошибок
catch (ParameterNotFoundException ex)
{
    Console.WriteLine($"Параметр {ex.ParameterName}({ex.ParameterNumber}) не найден.");
}
catch (FileNotFoundException)
{
    Console.WriteLine("Файл не найден");
}
catch (LexicalAnalyzerException ex)
{
    Console.WriteLine($"Лексическая ошибка в позиции {ex.Position}! {ex.Text}");
}
#endregion


void WriteSymbolsTable(string symbolsTableFilename, SymbolsTable table)
{
    using (var writer = new StreamWriter(symbolsTableFilename, false))
    {
        foreach (KeyValuePair<string, int> entry in table.Symbols)
        {
            writer.WriteLine($"{entry.Value} - {entry.Key}");
        }
    }       
}

void WriteTokens(string tokensFilename, List<Token> tokens)
{
    using (var writer = new StreamWriter(tokensFilename, false))
    {
        tokens.ForEach(token =>
        {
            writer.WriteLine(token);
        });
    }
}


string TakeArgOrThrow(int argNumber, string parameterName = "")
{
    try
    {
        return args[argNumber];
    }
    catch
    {
        throw new ParameterNotFoundException(parameterName, argNumber);
    }
}