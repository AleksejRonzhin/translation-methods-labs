namespace library.lexis.exceptions
{
    internal class InvalidSymbolException : Exception
    {
        public InvalidSymbolException(char symbol, int position)
        {
            Symbol = symbol;
            Position = position;
        }

        public char Symbol { get; }
        public int Position { get; }
    }
}
