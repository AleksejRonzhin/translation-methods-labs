namespace lab1.exceptions
{
    internal class OperationNotFoundException : Exception
    {
        public string Operation { get; }

        public OperationNotFoundException(string operation)
        {
            Operation = operation;
        }
    }
}