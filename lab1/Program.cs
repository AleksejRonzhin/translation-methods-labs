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
    var mode = takeArgOrThrow(0);
    ExpressionCreator expressionCreator = mode switch
    {
        "G" or "g" => InitGenerator(),
        "T" or "t" => InitTranslator(),
        _ => throw new NotImplementedException()
    };
    var expressions = expressionCreator.CreateExpressions();
    WriteExpressionsInFile(expressions);
    expressions.ForEach(expression => Console.WriteLine(expression));
}
catch (Exception ex)
{
    throw ex;
}

ExpressionCreator InitGenerator()
{
    var expressionCount = int.Parse(takeArgOrThrow(2));
    var minOperands = int.Parse(takeArgOrThrow(3));
    var maxOperands = int.Parse(takeArgOrThrow(4));
    return new ExpressionGenerator(expressionCount, minOperands, maxOperands, operators, operands);
}

ExpressionCreator InitTranslator()
{
    var expressions = ReadExpressionsFromFile();
    return new ExpressionTranslator(expressions, operators, operands);
}

void WriteExpressionsInFile(List<String> expressions)
{
    var outputFileName = takeArgOrThrow(1);
    using var writer = new StreamWriter(outputFileName, false);
    expressions.ForEach(expression => writer.WriteLine(expression));
}

List<string> ReadExpressionsFromFile()
{
    var inputFileName = args[2];
    List<string> expressions = new();
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

string takeArgOrThrow(int argNumber)
{
    return args[argNumber];
}
