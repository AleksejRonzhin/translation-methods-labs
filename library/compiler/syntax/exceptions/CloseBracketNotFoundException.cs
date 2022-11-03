namespace library.compiler.syntax.exceptions
{
    internal class CloseBracketNotFoundException : Exception
    {
        public int Position { get; }

        public CloseBracketNotFoundException(int position)
        {
            Position = position;
        }
    }
}
