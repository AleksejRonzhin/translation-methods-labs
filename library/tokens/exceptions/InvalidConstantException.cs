namespace library.tokens.exceptions
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
