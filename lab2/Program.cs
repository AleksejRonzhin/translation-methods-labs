using lab2.parameters;
using library.compiler;
using library.compiler.lexis.exceptions;
using library.compiler.semantic.exceptions;
using library.compiler.syntax.exceptions;
using library.output;
using library.parameters.exceptions;

Compiler compiler;

try
{
    var mode = new ModeParameter(TakeArgOrThrow(0, "режим")).GetValue();
    var inputFilename = new InputFilenameParameter(TakeArgOrThrow(1, "файл исходного выражения")).GetValue();
    using TextReader inputTextReader = new StreamReader(inputFilename);
    compiler = new(inputTextReader);

    Action action = mode switch
    {
        "LEX" or "lex" => LexicalAnalyzerAction,
        "SYN" or "syn" => SyntaxAnalyzerAction,
        "SEM" or "sem" => SemanticAnalyzerAction,
        "GEN1" or "gen1" => ThreeAddressCodeGeneratorAction,
        "GEN2" or "gen2" => PostfixNotationGeneratorAction,
        "GEN1_OPT" or "gen1_opt" => ThreeAddressCodeGeneratorWithOptimizationAction,
        "GEN2_OPT" or "gen2_opt" => PostfixNotationGeneratorWithOptimizationAction,
        _ => throw new Exception()
    };
    action.Invoke();

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
catch (ValidationException ex)
{
    Console.WriteLine("Неправильное значение параметра.\n" + ex.ValidationMessage);
}
catch (LexicalAnalyzerException ex)
{
    Console.WriteLine($"Лексическая ошибка в позиции {ex.Position}! {ex.Text}");
}
catch (SyntaxAnalyzerException ex)
{
    Console.WriteLine($"Синтаксическая ошибка в позиции {ex.Position}! {ex.Text}");
}
catch (SemanticAnalyzerException ex)
{
    Console.WriteLine($"Cемантическая ошибка в позиции {ex.Position}! {ex.Text}");
}
#endregion

void LexicalAnalyzerAction()
{
    var tokensFilename = new TokensFilenameParameter(TakeArgOrThrow(2, "файл токенов")).GetValue();
    var symbolsTableFilename = new SymbolsTableFilenameParameter(TakeArgOrThrow(3, "файл таблицы символов")).GetValue();

    TokensInFileWriter.WriteTokens(tokensFilename, compiler.Tokens);
    SymbolsTableInFileWriter.WriteSymbolsTable(symbolsTableFilename, compiler.SymbolsTable);
}

void SyntaxAnalyzerAction()
{
    var syntaxTreeFilename = new SyntaxTreeFilenameParameter(TakeArgOrThrow(2, "файл синтаксического дерева")).GetValue();
    SyntaxTreeInFileWriter.WriteSyntaxTree(syntaxTreeFilename, compiler.SyntaxTree);
}

void SemanticAnalyzerAction()
{
    var syntaxTreeFilename = new SyntaxTreeFilenameParameter(TakeArgOrThrow(2, "файл синтаксического дерева")).GetValue();
    var symbolsTableFilename = new SymbolsTableFilenameParameter(TakeArgOrThrow(3, "файл таблицы символов")).GetValue();

    SyntaxTreeInFileWriter.WriteSyntaxTree(syntaxTreeFilename, compiler.ModifierSyntaxTree);
    SymbolsTableInFileWriter.WriteSymbolsTable(symbolsTableFilename, compiler.SymbolsTable);
}

void ThreeAddressCodeGeneratorAction()
{
    GenerateThreeAddressCode(false);
}

void PostfixNotationGeneratorAction()
{
    GeneratePostfixNotation(false);
}


void PostfixNotationGeneratorWithOptimizationAction()
{
    GeneratePostfixNotation(true);
}

void ThreeAddressCodeGeneratorWithOptimizationAction()
{
    GenerateThreeAddressCode(true);
}

void GeneratePostfixNotation(bool withOptimization)
{
    var postfixFilename = "postfix.txt";
    PostfixNotationInFileWriter.WritePostfixNotation(postfixFilename, compiler.GetPostfixNotation(withOptimization));
}

void GenerateThreeAddressCode(bool withOptimization)
{
    var threeAddressCodeFilename = "portable_code.txt";
    var symbolsTableFilename = "symbols.txt";

    ThreeAddressCodeInFileWriter.WriteThreeAddressCode(threeAddressCodeFilename, compiler.GetThreeAddressCode(withOptimization));
    SymbolsTableInFileWriter.WriteSymbolsTable(symbolsTableFilename, compiler.SymbolsTable);
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