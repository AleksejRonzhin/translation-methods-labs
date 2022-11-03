namespace library.compiler.syntax.exceptions
{
    internal class OpenBracketNotFountException : Exception
    {
        public int Position { get; }

        public OpenBracketNotFountException(int position)
        {
            Position = position;
        }
    }
}
