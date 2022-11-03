using library.compiler.core.operations;

namespace library.compiler.core.symbols
{
    public class SymbolInfo
    {
        public int Index { get; }
        public string Name { get; }
        public OperandType OperandType { get; }

        public SymbolInfo(int index, string name, OperandType operandType)
        {
            Index = index;
            Name = name;
            OperandType = operandType;
        }

        public override string? ToString()
        {
            return $"{Index} - {Name}[{OperandType.ConvertToText()}]";
        }
    }
}