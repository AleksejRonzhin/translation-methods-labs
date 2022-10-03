namespace lab2.exceptions
{
    internal class LexicalAnalyzerException : Exception
    {
        public LexicalAnalyzerException(int position, string message)
        {
            Position = position;
            Text = message;
        }

        public int Position { get; }
        public string Text { get; }

    }
}
