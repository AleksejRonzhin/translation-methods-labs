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
                if (TryOptimizeOperand(firstOperand, lineResult, symbolsTable, lines.GetRange(i, lines.Count - i))) continue;
                var secondOperand = line.SecondOperand;
                if (secondOperand != null && TryOptimizeOperand(secondOperand, lineResult, symbolsTable, lines.GetRange(i, lines.Count - i))) continue;
            }
            Print(threeAddressCode, "After optimization:");
            return threeAddressCode;
        }

        private static void Print(ThreeAddressCode threeAddressCode, string title)
        {
            Console.WriteLine(title);
            threeAddressCode.Lines.ForEach(line =>
            {
                Console.WriteLine(line);
            });
        }

        private static bool TryOptimizeOperand(OperandToken operand, OperandToken replaceableOperand, SymbolsTable symbolsTable, List<ThreeAddressLine> lines)
        {
            if (operand is IdentifierToken)
            {
                SymbolInfo firstInfo = symbolsTable.GetByIndex((int)operand.AttributeValue);
                if (firstInfo.IsTemp)
                {
                    if (UsedLater(lines, operand))
                    {
                        ReplaceOperand(replaceableOperand, operand, lines);
                        DeleteSymbol(symbolsTable, (int)replaceableOperand.AttributeValue);
                        return true;
                    }
                }
            }
            return false;
        }

        private static void DeleteSymbol(SymbolsTable symbolsTable, int attributeValue)
        {
            symbolsTable.DeleteSymbolByIndex(attributeValue);
        }

        private static bool UsedLater(List<ThreeAddressLine> lines, OperandToken operand)
        {
            return OperandContains(lines, operand);
        }

        private static bool OperandContains(List<ThreeAddressLine> lines, OperandToken operand)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (operand == line.FirstOperand || operand == line.SecondOperand) return true;
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
