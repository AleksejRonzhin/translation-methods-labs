namespace lab1.exceptions
{
    internal class OperandNotFoundException : Exception
    {
        public int Operand { get; }


        public OperandNotFoundException(int operand)
        {
            Operand = operand;
        }
    }
}