using lab2.parameters;
using library;
using library.lexis.exceptions;
using library.output;
using library.parameters.exceptions;
using library.stages;
using library.syntax.exceptions;
using library.syntax.tree;
using library.tokens;


SymbolsTable symbolsTable = new();

try
{
    var mode = new ModeParameter(TakeArgOrThrow(0, "режим")).GetValue();
    var inputFilename = new InputFilenameParameter(TakeArgOrThrow(1, "файл исходного выражения")).GetValue();

    
    using TextReader inputTextReader = new StreamReader(inputFilename);
    StageCreator stageManagerPreparer = new(inputTextReader, symbolsTable);

    IStage stage = mode switch
    {
        "LEX" or "lex" => stageManagerPreparer.CreateLexicalAnalyzerStage(LexicalAnalyzerAction),
        "SYN" or "syn" => stageManagerPreparer.CreateSyntaxAnalyzerStage(SyntaxAnalyzerAction),
        "SEM" or "sem" => stageManagerPreparer.CreateSemanticAnalyzerStage(SemanticAnalyzerAction),
        _ => throw new Exception()
    };
    stage.Execute();
}
#region Обработка ошибок
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

void LexicalAnalyzerAction(List<TokenInfo> tokens)
{
    var tokensFilename = new TokensFilenameParameter(TakeArgOrThrow(2, "файл токенов")).GetValue();
    TokensInFileWriter.WriteTokens(tokensFilename, tokens);

    var symbolsTableFilename = new SymbolsTableFilenameParameter(TakeArgOrThrow(3, "файл таблицы символов")).GetValue();
    SymbolsTableInFileWriter.WriteSymbolsTable(symbolsTableFilename, symbolsTable);
}

void SyntaxAnalyzerAction(SyntaxTree syntaxTree)
{
    var syntaxTreeFilename = new SyntaxTreeFilenameParameter(TakeArgOrThrow(2, "файл синтаксического дерева")).GetValue();
    SyntaxTreeInFileWriter.WriteSyntaxTree(syntaxTreeFilename, syntaxTree);
}

void SemanticAnalyzerAction(SyntaxTree syntaxTree)
{
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