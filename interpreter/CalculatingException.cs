namespace interpreter
{
    internal class CalculatingException : Exception
    {
        public string Message { get; }
        public CalculatingException(string message)
        {
            Message = message;
        }
    }
}