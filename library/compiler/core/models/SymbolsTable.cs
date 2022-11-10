using System.Text;
using library.compiler.core.symbols;
using library.compiler.core.symbols.exceptions;

namespace library.compiler.core.models
{
    public class SymbolsTable
    {
        private readonly List<SymbolInfo> symbols = new();

        public int GetIndexOrAddSymbol(string symbolName, OperandType operandType)
        {
            var symbolInfo = symbols.SingleOrDefault(it => it.Name == symbolName);
            if (symbolInfo is null) return AddSymbol(symbolName, operandType);
            CheckOperandType(symbolInfo, operandType);
            return symbolInfo.Index;
        }

        private static void CheckOperandType(SymbolInfo symbolInfo, OperandType operandType)
        {
            if (operandType == OperandType.ANY) return;

            if (operandType != symbolInfo.OperandType)
            {
                throw new TokenAlreadyDefinedWithAnotherTypeException(symbolInfo.OperandType);
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

        internal SymbolInfo GetById(int attributeValue)
        {
            var symbolInfo = symbols.SingleOrDefault(symbol => symbol.Index == attributeValue);
            if (symbolInfo != null) return symbolInfo;
            throw new Exception();
        }
    }
}