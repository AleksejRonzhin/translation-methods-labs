namespace library.symbols
{
    internal class TokenAlreadyDefinedWithAnotherTypeException : Exception
    {
        public TokenAlreadyDefinedWithAnotherTypeException(OperandType operandType)
        {
            OperandType = operandType;
        }

        public OperandType OperandType{ get; }
    }
}