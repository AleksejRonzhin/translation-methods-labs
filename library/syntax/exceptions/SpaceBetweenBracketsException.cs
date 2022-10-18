namespace library.syntax.exceptions
{
    internal class SpaceBetweenBracketsException : Exception
    {
        public int OpenBracketPosition { get; }

        public SpaceBetweenBracketsException(int openBracketPosition)
        {
            OpenBracketPosition = openBracketPosition;
        }
    }
}
