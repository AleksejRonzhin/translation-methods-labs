using library.compiler.core.operations;

namespace library.compiler.core.symbols.exceptions
{
    internal class TokenAlreadyDefinedWithAnotherTypeException : Exception
    {
        public TokenAlreadyDefinedWithAnotherTypeException(OperandType operandType)
        {
            OperandType = operandType;
        }

        public OperandType OperandType { get; }
    }
}