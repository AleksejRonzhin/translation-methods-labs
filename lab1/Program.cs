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
    var mode = GetMode();
    ExpressionCreator expressionCreator = mode switch
    {
        "G" or "g" => InitGenerator(),
        "T" or "t" => InitTranslator(),
        _ => throw new ValidationException("Значение режима работы некорректно")
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
    Console.WriteLine("Неправильное значение параметра.\n" + ex.ValidationMessage);
}

ExpressionCreator InitGenerator()
{
    var expressionCount = GetExpressionCount();
    var (minOperands, maxOperands) = GetOperandRange();
    return new ExpressionGenerator(expressionCount, minOperands, maxOperands, operators, operands);
}

ExpressionCreator InitTranslator()
{
    var expressions = ReadExpressionsFromFile();
    return new ExpressionTranslator(expressions, operators, operands);
}

void WriteExpressionsInFile(List<String> expressions)
{
    var outputFileName = GetOutputFilename();
    using var writer = new StreamWriter(outputFileName, false);
    expressions.ForEach(expression => writer.WriteLine(expression));
}

List<string> ReadExpressionsFromFile()
{
    var inputFileName = GetInputFilename();
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

string GetMode()
{
    return TakeArgOrThrow(0, MapMode, "режим программы");
}

int GetExpressionCount()
{
    return TakeArgOrThrow(2, MapExpressionCount, "количество выражений");
}

(int min, int max) GetOperandRange()
{
    var min = TakeArgOrThrow(3, MapMinOperand, "минимальное количество операндов");
    var max = TakeArgOrThrow(4, MapMaxOperand, "максимальное количество операндов");
    if (min > max) throw new ValidationException($"Минимальное значение ({min}) больше максимального ({max}).");
    return (min, max);
}

string GetOutputFilename()
{
    return TakeArgOrThrow(1, MapInputFilename, "файл записи результатов");
}

string GetInputFilename()
{
    return TakeArgOrThrow(2, MapOutputFilename, "файл исходных данных");
}

T TakeArgOrThrow<T>(int argNumber, Func<string, T> mapper, string parameterName = "")
{
    try
    {
        return mapper(args[argNumber]);
    }
    catch (ValidationException ex)
    {
        throw new ValidationException($"Значение параметра {parameterName} некорректно.\n" + ex.ValidationMessage);
    }
    catch
    {
        throw new ParameterNotFoundException(parameterName, argNumber);
    }
}

string MapMode(string arg)
{
    return ValidateStringIsOneOfValues(arg, "g", "G", "t", "T");
}

int MapExpressionCount(string arg)
{
    return ValidateIntInRange(CastStringToIntOrThrow(arg), 0, 50);
}

int MapMinOperand(string arg)
{
    return ValidateIntInRange(CastStringToIntOrThrow(arg), 0, 10);
}

int MapMaxOperand(string arg)
{
    return ValidateIntInRange(CastStringToIntOrThrow(arg), 0, 10);
}

string MapInputFilename(string arg)
{
    return arg;
}

string MapOutputFilename(string arg)
{
    return arg;
}

int CastStringToIntOrThrow(string arg)
{
    try
    {
        return int.Parse(arg);
    }
    catch
    {
        throw new ValidationException($"{arg} не является целым числом.");
    }
}

string ValidateStringIsOneOfValues(string arg, params string[] values)
{
    if (values.Contains(arg)) return arg;
    throw new ValidationException($"Значение {arg} является не допустимым. Допустимые значения: {string.Join(", ", values)}.");
}

int ValidateIntInRange(int value, int minValue, int maxValue)
{
    var isRigthValue = value >= minValue && value <= maxValue;
    if (isRigthValue) return value;
    else throw new ValidationException($"{value} не лежит в диапазоне [{minValue}, {maxValue}].");
}