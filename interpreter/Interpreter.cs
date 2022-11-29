using library.compiler.core.models;
using library.compiler.core.symbols;
using library.compiler.core.tokens;
using System.Globalization;

namespace interpreter
{
    public class Interpreter
    {
        private readonly ThreeAddressCode _threeAddressCode;
        private readonly SymbolsTable _symbolsTable;

        public Interpreter(ThreeAddressCode threeAddressCode, SymbolsTable symbolsTable)
        {
            _threeAddressCode = threeAddressCode;
            _symbolsTable = symbolsTable;
        }

        public string Start()
        {
            InputValues();
            Calculate();
            return GetResult();
        }

        private void InputValues()
        {
            _symbolsTable.Symbols.ForEach(symbol =>
            {
                if (!symbol.IsTemp)
                {
                    while (true)
                    {
                        Console.WriteLine($"Input {symbol.Name}({symbol.OperandType})");
                        var input = Console.ReadLine();

                        if ((symbol.OperandType == OperandType.INTEGER && int.TryParse(input, out var _))
                        || (symbol.OperandType == OperandType.REAL && double.TryParse(input, NumberStyles.Float, ConstantToken.numberFormatInfo, out var _)))
                        {
                            symbol.Value = input;
                            break;
                        }
                    }
                }
            });
        }

        private void Calculate()
        {
            try
            {
                _threeAddressCode.Lines.ForEach(line =>
                {
                    string operationCode = line.OperationCode;
                    var resultToken = line.Result;
                    var firstOperand = line.FirstOperand;
                    var secondOperand = line.SecondOperand;
                    string result = operationCode switch
                    {
                        "i2r" => CastOperation(firstOperand),
                        "add" => AddOperation(firstOperand, secondOperand!),
                        "sub" => SubOperation(firstOperand, secondOperand!),
                        "mul" => MullOperation(firstOperand, secondOperand!),
                        "div" => DivOperation(firstOperand, secondOperand!),
                        _ => throw new Exception()
                    };
                    SetOperandValue(resultToken, result);
                });
            } catch(DivideByZeroException e)
            {
                throw new CalculatingException("Деление на ноль");
            }
            
        }

        private string AddOperation(OperandToken firstOperand, OperandToken secondOperand)
        {
            return MathOperation(firstOperand, secondOperand,
                (first, second) => { return first + second; },
                (first, second) => { return first + second; });
        }

        private string SubOperation(OperandToken firstOperand, OperandToken secondOperand)
        {
            return MathOperation(firstOperand, secondOperand,
                (first, second) => { return first - second; },
                (first, second) => { return first - second; });
        }

        private string MullOperation(OperandToken firstOperand, OperandToken secondOperand)
        {
            return MathOperation(firstOperand, secondOperand,
                (first, second) => { return first * second; },
                (first, second) => { return first * second; });
        }
        private string DivOperation(OperandToken firstOperand, OperandToken secondOperand)
        {

            return MathOperation(firstOperand, secondOperand,
                (first, second) => { 
                    if(second == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return first / second; },
                (first, second) => {
                    if (second == 0.0)
                    {
                        throw new DivideByZeroException();
                    }
                    return first / second; });
        }

        private string MathOperation(OperandToken firstOperand, OperandToken secondOperand, Func<int, int, int> intFunc, Func<double, double, double> doubleFunc)
        {
            (OperandType firstType, string firstValue) = GetOperandValue(firstOperand);
            (OperandType secondType, string secondValue) = GetOperandValue(secondOperand);
            if (firstType == OperandType.INTEGER)
            {
                int first = int.Parse(firstValue);
                int second = int.Parse(secondValue);
                return intFunc(first, second).ToString();
            }
            else
            {
                double first = double.Parse(firstValue, ConstantToken.numberFormatInfo);
                double second = double.Parse(secondValue, ConstantToken.numberFormatInfo);
                string result = doubleFunc(first, second).ToString(ConstantToken.numberFormatInfo);
                if (int.TryParse(result, out var _)) result += ".0";
                return result;
            }
        }

        private string CastOperation(OperandToken firstOperand)
        {
            (OperandType _, string firstValue) = GetOperandValue(firstOperand);
            return firstValue + ".0";
        }

        private (OperandType, string) GetOperandValue(OperandToken operandToken)
        {
            if (operandToken is IdentifierToken)
            {
                SymbolInfo symbolInfo = _symbolsTable.GetByOperandToken(operandToken);
                string? value = symbolInfo.Value;
                if (value == null) throw new Exception();
                OperandType operandType = symbolInfo.OperandType;
                return (operandType, value);
            }
            if (operandToken is ConstantToken constantToken)
            {
                return (constantToken.GetOperandType(), constantToken.TokenName);
            }
            throw new Exception();
        }

        private void SetOperandValue(OperandToken operandToken, String value)
        {
            _symbolsTable.GetByOperandToken(operandToken).Value = value;
        }

        private string GetResult()
        {
            OperandToken resultToken = _threeAddressCode.Lines.Last().Result;
            (_, string result) = GetOperandValue(resultToken);
            return result;
        }
    }
}
