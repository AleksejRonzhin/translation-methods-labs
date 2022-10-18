using lab2.parameters;
using library;
using library.exceptions;
using library.lexis;
using library.lexis.exceptions;
using library.syntax;
using library.syntax.exceptions;
using library.tokens;

try
{
    var mode = new ModeParameter(TakeArgOrThrow(0, "режим")).GetValue();
    var inputFilename = new InputFilenameParameter(TakeArgOrThrow(1, "файл исходного выражения")).GetValue();
    
    using (TextReader inputTextReader = new StreamReader(inputFilename))
    {
        (List<TokenInfo> tokens, SymbolsTable table) = LexicalAnalyzer.Analyze(inputTextReader);
        //tokens.ForEach(token => Console.WriteLine(token));
        
        switch (mode)
        {
            case "LEX":
            case "lex":
                var tokensFilename = new TokensFilenameParameter(TakeArgOrThrow(2, "файл токенов")).GetValue();
                var symbolsTableFilename = new SymbolsTableFilenameParameter(TakeArgOrThrow(3, "файл таблицы символов")).GetValue();
                WriteTokens(tokensFilename, tokens);
                WriteSymbolsTable(symbolsTableFilename, table);
                break;
            case "SYN":
            case "syn":
                SyntaxTree syntaxTree = SyntaxAnalyzer.Analyze(tokens);
                var syntaxTreeFilename = new SyntaxTreeFilenameParameter(TakeArgOrThrow(2, "файл синтаксического дерева")).GetValue();
                break;
        }
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
catch(SyntaxAnalyzerException ex)
{
    Console.WriteLine($"Синтаксическая ошибка в позиции {ex.Position}! {ex.Text}");
}
catch (ValidationException ex)
{
    Console.WriteLine("Неправильное значение параметра.\n" + ex.ValidationMessage);
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

void WriteTokens(string tokensFilename, List<TokenInfo> tokens)
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