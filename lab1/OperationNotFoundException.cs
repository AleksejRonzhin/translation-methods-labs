namespace lab1
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