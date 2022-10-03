namespace lab2.exceptions
{
    internal class InvalidConstantException : Exception
    {
        public InvalidConstantException(string constant)
        {
            Constant = constant;
        }

        public string Constant { get; }
    }
}
