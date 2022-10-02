namespace lab1.exceptions
{
    public class OperandNotIntegerException : Exception
    {
        public OperandNotIntegerException(string operand)
        {
            Operand = operand;
        }

        public string Operand { get; }
    }
}
