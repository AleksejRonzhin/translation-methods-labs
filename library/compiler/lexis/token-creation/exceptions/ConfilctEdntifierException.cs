using library.compiler.core.operations;

namespace library.tokens.creation.exceptions
{
    internal class ConflictIdentifierTypesException: Exception
    {
        public int Position { get; }
        public string TokenName { get; }
        public OperandType FirstOperandType { get; }
        public OperandType SecondOperandType { get; }

        public ConflictIdentifierTypesException(int position, string tokenName, OperandType firstOperandType, OperandType secondOperandType)
        {
            Position = position;
            TokenName = tokenName;
            FirstOperandType = firstOperandType;
            SecondOperandType = secondOperandType;
        }
    }
}
