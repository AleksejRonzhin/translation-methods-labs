using lab1.exceptions;
using lab1.expressions;

var operators = new Dictionary<string, string>()
{
    {"+", "плюс" },
    {"-", "минус" },
    {"*", "умножить на" },
    {"/", "делить на" }
};
var operands = new Dictionary<int, string>()
{
    {1, "один"},
    {2, "два"},
    {3, "три"},
    {4, "четыре"},
    {5, "пять"},
    {6, "шесть"},
    {7, "семь"},
    {8, "восемь"},
    {9, "девять"}
};

try
{
    var mode = takeArgOrThrow(0, "режим");
    ExpressionCreator expressionCreator = mode switch
    {
        "G" or "g" => InitGenerator(),
        "T" or "t" => InitTranslator(),
        _ => throw new ValidationException("Доступные режимы утилиты: " +
            $"T(t) - режим трансляции, G(g) - режим генерации. Найдено {mode}")
    };
    var expressions = expressionCreator.CreateExpressions();
    WriteExpressionsInFile(expressions);
}
catch (ParameterNotFoundException ex)
{
    Console.WriteLine($"Параметр {ex.ParameterName}({ex.ParameterNumber}) не найден");
}
catch (InputFileNotFoundException)
{
    Console.WriteLine("Файл исходных данных не найден");
}
catch (ValidationException ex)
{
    Console.WriteLine("Неправильное значение параметра:\n" + ex.ValidationMessage);
}

ExpressionCreator InitGenerator()
{
    var expressionCount = int.Parse(takeArgOrThrow(2, "количество выражений"));
    var minOperands = int.Parse(takeArgOrThrow(3, "минимальное количество операндов"));
    var maxOperands = int.Parse(takeArgOrThrow(4, "максимальное количество операндов"));
    return new ExpressionGenerator(expressionCount, minOperands, maxOperands, operators, operands);
}

ExpressionCreator InitTranslator()
{
    var expressions = ReadExpressionsFromFile();
    return new ExpressionTranslator(expressions, operators, operands);
}

void WriteExpressionsInFile(List<String> expressions)
{
    var outputFileName = takeArgOrThrow(1, "файл записи результатов");
    using var writer = new StreamWriter(outputFileName, false);
    expressions.ForEach(expression => writer.WriteLine(expression));
}

List<string> ReadExpressionsFromFile()
{
    var inputFileName = takeArgOrThrow(2, "файл исходных данных");
    List<string> expressions = new();
    try
    {
        using (var reader = new StreamReader(inputFileName))
        {
            var expression = reader.ReadLine();
            while (expression != null)
            {
                expressions.Add(expression);
                expression = reader.ReadLine();
            }
        }
        return expressions;
    }
    catch (FileNotFoundException)
    {
        throw new InputFileNotFoundException();
    }
}

string takeArgOrThrow(int argNumber, string parameterName = "")
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
