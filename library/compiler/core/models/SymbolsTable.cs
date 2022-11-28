using System.Text;
using library.compiler.core.symbols;
using library.compiler.core.symbols.exceptions;

namespace library.compiler.core.models
{
    [Serializable]
    public class SymbolsTable
    {
        public List<SymbolInfo> Symbols { get; } = new();

        public int GetIndexOrAddSymbol(string symbolName, OperandType operandType, bool isTemp)
        {
            var symbolInfo = Symbols.SingleOrDefault(it => it.Name == symbolName);
            if (symbolInfo is null) return AddSymbol(symbolName, operandType, isTemp);
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

        public void DeleteSymbolByIndex(int index)
        {
            Symbols.Remove(GetByIndex(index));
        }

        private int AddSymbol(string symbolName, OperandType operandType, bool isTemp)
        {
            if (operandType == OperandType.ANY) operandType = OperandType.INTEGER;
            var symbolInfo = new SymbolInfo(Symbols.Count + 1, symbolName, operandType, isTemp);
            Symbols.Add(symbolInfo);
            return symbolInfo.Index;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            Symbols.ForEach(symbolInfo => stringBuilder.AppendLine(symbolInfo.ToString()));
            return stringBuilder.ToString();
        }

        internal SymbolInfo GetByIndex(int attributeValue)
        {
            var symbolInfo = Symbols.SingleOrDefault(symbol => symbol.Index == attributeValue);
            if (symbolInfo != null) return symbolInfo;
            throw new Exception();
        }
    }
}