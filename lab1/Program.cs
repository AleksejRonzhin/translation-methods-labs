using lab1;
using lab1.expressions;
using lab1.parameters;
using library.exceptions;
using library.parameters.validators;

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
    var mode = new ModeParameter(TakeArgOrThrow(0, "режим программы")).GetValue();
    ExpressionCreator expressionCreator = mode switch
    {
        "G" or "g" => InitGenerator(),
        "T" or "t" => InitTranslator(),
        _ => throw new ValidationException("Значение режима работы некорректно")
    };
    var expressions = expressionCreator.CreateExpressions();
    WriteExpressionsInFile(expressions);
}
#region Обработка ошибок
catch (ParameterNotFoundException ex)
{
    Console.WriteLine($"Параметр {ex.ParameterName}({ex.ParameterNumber}) не найден.");
}
catch (InputFileNotFoundException)
{
    Console.WriteLine("Файл исходных данных не найден.");
}
catch (ValidationException ex)
{
    Console.WriteLine("Неправильное значение параметра.\n" + ex.ValidationMessage);
}
catch (OperandNotIntegerException ex)
{
    Console.WriteLine($"Операнд {ex.Operand} не является числом");
}
catch (OperandNotFoundException ex)
{
    Console.WriteLine($"Операнд {ex.Operand} не найден");
}
catch(OperationNotFoundException ex)
{
    Console.WriteLine($"Операция {ex.Operation} не найдена");
}
#endregion

ExpressionCreator InitGenerator()
{
    var expressionCount = new ExpressionCountParameter(TakeArgOrThrow(2, "количество выражений")).GetValue();
    var minOperands = new MinOperandsParameter(TakeArgOrThrow(3, "минимальное количество операндов")).GetValue();
    var maxOperands = new MaxOperandsParameter(TakeArgOrThrow(4, "максимальное количество операндов")).GetValue();
    var range = (minOperands, maxOperands);
    new IntRangeLimitsValidator().Validate(range);
    return new ExpressionGenerator(expressionCount, range, operators, operands);
}

ExpressionCreator InitTranslator()
{
    var expressions = ReadExpressionsFromFile();
    return new ExpressionTranslator(expressions, operators, operands);
}

void WriteExpressionsInFile(List<string> expressions)
{
    var outputFileName = new OutputFilenameParameter(TakeArgOrThrow(1, "файл вывода")).GetValue();
    using var writer = new StreamWriter(outputFileName, false);
    expressions.ForEach(expression => writer.WriteLine(expression));
}

List<string> ReadExpressionsFromFile()
{
    var inputFileName = new OutputFilenameParameter(TakeArgOrThrow(2, "файл значений")).GetValue();
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