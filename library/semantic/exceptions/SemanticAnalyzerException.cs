namespace library.semantic.exceptions
{
    public class SemanticAnalyzerException : Exception
    {
        public string Text { get; }
        public int Position { get; }

        public SemanticAnalyzerException(int position, string text)
        {
            Text = text;
            Position = position;
        }
    }
}
