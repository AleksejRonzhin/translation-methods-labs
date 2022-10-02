using lab2;
using lab2.parameters;
using library.exceptions;

try
{
    var inputFilename = new InputFilenameParameter(TakeArgOrThrow(0, "файл исходного выражения")).GetValue();
    var tokensFilename = new TokensFilenameParameter(TakeArgOrThrow(1, "файл токенов")).GetValue();
    var tableTokenFilename = new TableSymbolsFilenameParameter(TakeArgOrThrow(2, "файл таблицы символов")).GetValue();
   
    using (TextReader inputTextReader = new StreamReader(inputFilename))
    {
        new LexicalAnalyzer().Analyze(inputTextReader);
    }
    // читаем файл
    // бежим по подстрокам, создаем токены и символы в таблице
    // записываем в файлы

}
catch (ParameterNotFoundException ex)
{
    Console.WriteLine($"Параметр {ex.ParameterName}({ex.ParameterNumber}) не найден.");
}
catch (FileNotFoundException)
{
    Console.WriteLine("Файл не найден");
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