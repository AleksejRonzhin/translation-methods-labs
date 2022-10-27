using lab2.parameters;
using library;
using library.lexis;
using library.lexis.exceptions;
using library.output;
using library.parameters.exceptions;
using library.syntax;
using library.syntax.exceptions;
using library.syntax.tree;
using library.stages;
using library.tokens;



try
{
    var mode = new ModeParameter(TakeArgOrThrow(0, "режим")).GetValue();
    var inputFilename = new InputFilenameParameter(TakeArgOrThrow(1, "файл исходного выражения")).GetValue();
    using TextReader inputTextReader = new StreamReader(inputFilename);

    switch (mode)
    {
        case "LEX":
        case "lex":
            Lexical(inputTextReader);
            break;
        case "SYN":
        case "syn":
            Syntax(inputTextReader);
            break;
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
catch (SyntaxAnalyzerException ex)
{
    Console.WriteLine($"Синтаксическая ошибка в позиции {ex.Position}! {ex.Text}");
}
catch (ValidationException ex)
{
    Console.WriteLine("Неправильное значение параметра.\n" + ex.ValidationMessage);
}
#endregion

void Lexical(TextReader inputTextReader)
{
    (List<TokenInfo> tokens, SymbolsTable table) = ((List<TokenInfo>, SymbolsTable)) StageExecutor.Execute(Stage.LEX, inputTextReader);
    
    tokens.ForEach(token => Console.WriteLine(token));

    var tokensFilename = new TokensFilenameParameter(TakeArgOrThrow(2, "файл токенов")).GetValue();
    var symbolsTableFilename = new SymbolsTableFilenameParameter(TakeArgOrThrow(3, "файл таблицы символов")).GetValue();
    TokensInFileWriter.WriteTokens(tokensFilename, tokens);
    SymbolsTableInFileWriter.WriteSymbolsTable(symbolsTableFilename, table);
}

void Syntax(TextReader inputTextReader)
{
    SyntaxTree syntaxTree = (SyntaxTree) StageExecutor.Execute(Stage.SYN, inputTextReader);

    Console.WriteLine(SyntaxTreePrinter.Print(syntaxTree));

    var syntaxTreeFilename = new SyntaxTreeFilenameParameter(TakeArgOrThrow(2, "файл синтаксического дерева")).GetValue();
    SyntaxTreeInFileWriter.WriteSyntaxTree(syntaxTreeFilename, syntaxTree);
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