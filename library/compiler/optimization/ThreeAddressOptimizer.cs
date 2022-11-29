using library.compiler.core.models;
using library.compiler.core.symbols;
using library.compiler.core.tokens;

namespace library.compiler.optimization
{
    internal class ThreeAddressCodeOptimizer
    {
        public static ThreeAddressCode Optimize(ThreeAddressCode threeAddressCode, SymbolsTable symbolsTable)
        {
            Print(threeAddressCode, "Before optimization:");
            var lines = threeAddressCode.Lines;
            for (int i = 1; i < lines.Count; i++)
            {
                var line = lines[i];
                var lineResult = line.Result;
                var firstOperand = line.FirstOperand;
                var nextLines = lines.GetRange(i, lines.Count - i);
                if (TryOptimizeOperand(firstOperand, lineResult, symbolsTable, nextLines)) continue;

                var secondOperand = line.SecondOperand;
                if (secondOperand != null 
                    && TryOptimizeOperand(secondOperand, lineResult, symbolsTable, nextLines)) continue;
            }
            Print(threeAddressCode, "After optimization:");
            return threeAddressCode;
        }

        private static OperandType GetOperandType(OperandToken operandToken, SymbolsTable symbolsTable)
        {
            if (operandToken is IdentifierToken)
            {
                int? index = operandToken.AttributeValue;
                if (index == null) throw new Exception();
                return symbolsTable.GetByIndex((int)index).OperandType;
            }
            if (operandToken is ConstantToken constantToken)
            {
                return constantToken.GetOperandType();
            }
            return OperandType.NOT_DEFINED;
        }

        private static void Print(ThreeAddressCode threeAddressCode, string title)
        {
            Console.WriteLine(title);
            threeAddressCode.Lines.ForEach(line =>
            {
                Console.WriteLine(line);
            });
        }

        private static bool TryOptimizeOperand(OperandToken operand, OperandToken replaceableOperand, SymbolsTable symbolsTable, List<ThreeAddressLine> nextLines)
        {
            if (GetOperandType(replaceableOperand, symbolsTable) != GetOperandType(operand, symbolsTable)) return false;

            if (operand is IdentifierToken)
            {
                SymbolInfo operandInfo = symbolsTable.GetByOperandToken(operand);
                if (operandInfo.IsTemp && NotUsedLater(nextLines.GetRange(1, nextLines.Count - 1), operand))
                {
                    ReplaceOperand(replaceableOperand, operand, nextLines);
                    DeleteExtraTemp(symbolsTable, replaceableOperand);
                    return true;
                }
            }
            return false;
        }

        private static void DeleteExtraTemp(SymbolsTable symbolsTable, OperandToken operandToken)
        {
            symbolsTable.DeleteByOperandToken(operandToken);
        }

        private static bool NotUsedLater(List<ThreeAddressLine> lines, OperandToken operand)
        {
            return !OperandContains(lines, operand);
        }

        private static bool OperandContains(List<ThreeAddressLine> lines, OperandToken operand)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if(operand == line.FirstOperand)
                {
                    Console.WriteLine($"{operand} == {line.FirstOperand}");
                    return true;
                }
                if (operand == line.SecondOperand)
                {
                    Console.WriteLine($"{operand} == {line.SecondOperand}");
                    return true;
                }
            }
            return false;
        }

        private static void ReplaceOperand(OperandToken source, OperandToken target, List<ThreeAddressLine> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                ThreeAddressLine line = lines[i];
                if (line.Result == source) line.Result = target;
                if (line.FirstOperand == source) line.FirstOperand = target;
                if (line.SecondOperand == source) line.SecondOperand = target;
            }
        }
    }
}
