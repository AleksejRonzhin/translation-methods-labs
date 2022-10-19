namespace library.tokens.creation.exceptions
{
    internal class OperationNotFoundException : Exception
    {
        public string Lexeme { get; }
        public int Position { get; }

        public OperationNotFoundException(string lexeme, int position)
        {
            Lexeme = lexeme;
            Position = position;
        }
    }
}