using library.compiler.core.models;
using library.compiler.core.symbols;
using library.compiler.core.tokens;
using System.Runtime.Serialization.Formatters.Binary;
internal class Program
{
    private static void Main(string[] args)
    {
        var binaryFileName = args[0];
        (ThreeAddressCode threeAddressCode, SymbolsTable symbolsTable) = ReadBinaryFile(binaryFileName);
        InputValues(symbolsTable);
        Calculate(threeAddressCode, symbolsTable);
        string result = GetResult(threeAddressCode, symbolsTable);
        Console.WriteLine($"Result: {result}");
    }

    private static (ThreeAddressCode, SymbolsTable) ReadBinaryFile(string binaryFileName)
    {
        BinaryFormatter formatter = new();
        FileStream fileStream = new(binaryFileName, FileMode.Open);
        var threeAddressCode = (ThreeAddressCode)formatter.Deserialize(fileStream);
        var symbolsTable = (SymbolsTable)formatter.Deserialize(fileStream);
        fileStream.Close();
        return (threeAddressCode, symbolsTable);
    }

    private static void InputValues(SymbolsTable symbolsTable)
    {
        symbolsTable.Symbols.ForEach(symbol =>
        {
            if (!symbol.IsTemp)
            {
                Console.WriteLine($"input {symbol.Name} {symbol.OperandType}");
                //var input = Console.ReadLine();
                symbol.Value = "1";
                Console.WriteLine(symbol.Value);
            }
        });
    }

    private static void Calculate(ThreeAddressCode threeAddressCode, SymbolsTable symbolsTable)
    {
        Console.WriteLine(threeAddressCode);
        Console.WriteLine(symbolsTable);
        threeAddressCode.Lines.ForEach(line =>
        {
            string operationCode = line.OperationCode;
            var resultToken = line.Result;
            string result = operationCode switch
            {
                "i2r" => CastOperation(line, symbolsTable),
                "add" => AddOperation(line, symbolsTable),
                "sub" => SubOperation(line, symbolsTable),
                "mul" => MullOperation(line, symbolsTable),
                "div" => DivOperation(line, symbolsTable),
                _ => throw new Exception()
            };
            Console.WriteLine(result);
            SetOperandValue(resultToken, symbolsTable, result);
        });
    }

    private static string AddOperation(ThreeAddressLine line, SymbolsTable symbolsTable)
    {
        return MathOperation(line, symbolsTable, 
            (first, second) => { return first + second; },
            (first, second) => { return first + second; });
    }

    private static string SubOperation(ThreeAddressLine line, SymbolsTable symbolsTable)
    {
        return MathOperation(line, symbolsTable,
            (first, second) => { return first - second; },
            (first, second) => { return first - second; });
    }

    private static string MullOperation(ThreeAddressLine line, SymbolsTable symbolsTable)
    {
        return MathOperation(line, symbolsTable,
            (first, second) => { return first * second; },
            (first, second) => { return first * second; });
    }
    private static string DivOperation(ThreeAddressLine line, SymbolsTable symbolsTable)
    {
        return MathOperation(line, symbolsTable,
            (first, second) => { return first / second; },
            (first, second) => { return first / second; });
    }

    private static string MathOperation(ThreeAddressLine line, SymbolsTable symbolsTable, Func<int, int, int> intFunc, Func<double, double, double> doubleFunc)
    {
        (OperandType firstType, string firstValue) = GetOperandValue(line.FirstOperand, symbolsTable);
        (OperandType secondType, string secondValue) = GetOperandValue(line.SecondOperand, symbolsTable);
        if(firstType == OperandType.INTEGER)
        {
            int first = int.Parse(firstValue);
            int second = int.Parse(secondValue);
            return intFunc(first, second).ToString();
        } else
        {
            double first = double.Parse(firstValue, ConstantToken.numberFormatInfo);
            double second = double.Parse(secondValue, ConstantToken.numberFormatInfo);
            string result = doubleFunc(first, second).ToString();
            if (!result.Contains('.')) result += ".0";
            return result;
        }
    }


    private static string CastOperation(ThreeAddressLine line, SymbolsTable symbolsTable)
    {
        (OperandType _, string firstValue) = GetOperandValue(line.FirstOperand, symbolsTable);
        return firstValue + ".0";
    }

    private static (OperandType, string) GetOperandValue(OperandToken operandToken, SymbolsTable symbolsTable)
    {
        if(operandToken is IdentifierToken)
        {
            int? operandIndex = operandToken.AttributeValue;
            if (operandIndex == null) throw new Exception();
            SymbolInfo symbolInfo = symbolsTable.GetByIndex((int)operandIndex);
            string? value = symbolInfo.Value;
            if (value == null) throw new Exception();
            OperandType operandType = symbolInfo.OperandType;
            return (operandType, value);
        }
        if(operandToken is ConstantToken constantToken)
        {
            return (constantToken.GetOperandType(), constantToken.TokenName);
        }
        throw new Exception();
    }

    private static void SetOperandValue(OperandToken operandToken, SymbolsTable symbolsTable, String value)
    {
        symbolsTable.GetByIndex((int)operandToken.AttributeValue).Value = value;
    }

    private static string GetResult(ThreeAddressCode threeAddressCode, SymbolsTable symbolsTable)
    {
        OperandToken resultToken = threeAddressCode.Lines.Last().Result;
        (_, string result) = GetOperandValue(resultToken, symbolsTable);
        return result;
    }
}