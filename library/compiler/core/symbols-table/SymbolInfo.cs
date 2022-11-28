using library.compiler.core.models;

namespace library.compiler.core.symbols
{
    public class SymbolInfo
    {
        public int Index { get; }
        public string Name { get; }
        public OperandType OperandType { get; }

        public bool IsTemp { get; }

        public SymbolInfo(int index, string name, OperandType operandType, bool isTemp)
        {
            Index = index;
            Name = name;
            OperandType = operandType;
            IsTemp = isTemp;
        }

        public override string? ToString()
        {
            return $"{Index} - {Name}[{OperandType.ConvertToText()}]";
        }
    }
}