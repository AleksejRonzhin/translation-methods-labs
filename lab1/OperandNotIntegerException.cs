namespace lab1
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
