namespace library.compiler.syntax.exceptions
{
    public class SyntaxAnalyzerException : Exception
    {
        public int Position { get; }
        public string Text { get; }

        public SyntaxAnalyzerException(int position, string text)
        {
            Position = position;
            Text = text;
        }
    }
}
