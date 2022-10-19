namespace library.tokens.creation.exceptions
{
    internal class InvalidConstantException : Exception
    {
        public InvalidConstantException(string constant, int position)
        {
            Constant = constant;
            Position = position;
        }

        public string Constant { get; }
        public int Position { get; }
    }
}
