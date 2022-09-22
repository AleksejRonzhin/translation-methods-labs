namespace lab1
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