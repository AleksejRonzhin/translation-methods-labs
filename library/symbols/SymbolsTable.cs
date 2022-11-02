using System.Text;

namespace library.symbols
{
    public class SymbolsTable
    {
        private readonly List<SymbolInfo> symbols = new();

        public int GetOrAddSymbol(string symbolName, OperandType operandType)
        {
            var symbolInfo = symbols.SingleOrDefault(it => it.Name == symbolName);
            if (symbolInfo is null) return AddSymbol(symbolName, operandType);
            CheckOperandType(symbolInfo, operandType);
            return symbolInfo.Index;
        }

        private void CheckOperandType(SymbolInfo symbolInfo, OperandType operandType)
        {
            if (operandType == OperandType.ANY) return;

            if (operandType != symbolInfo.OperandType)
            {
                throw new Exception(); // Уже объявлен с другим типом
            }
        }

        private int AddSymbol(string symbolName, OperandType operandType)
        {
            if (operandType == OperandType.ANY) operandType = OperandType.INTEGER;
            var symbolInfo = new SymbolInfo(symbols.Count + 1, symbolName, operandType);
            symbols.Add(symbolInfo);
            return symbolInfo.Index;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            symbols.ForEach(symbolInfo => stringBuilder.AppendLine(symbolInfo.ToString()));
            return stringBuilder.ToString();
        }
    }
}