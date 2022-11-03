namespace library.tokens.creation.exceptions
{
    internal class InvalidIdentifierException : Exception
    {
        public string Identifier { get; }

        public int Position { get; }

        public InvalidIdentifierException(string identifier, int position)
        {
            Identifier = identifier;
            Position = position;
        }
    }
}
