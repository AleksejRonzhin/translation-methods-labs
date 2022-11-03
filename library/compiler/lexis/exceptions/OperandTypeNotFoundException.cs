namespace library.compiler.lexis.exceptions
{
    internal class OperandTypeNotDefinedException : Exception
    {
        public string OperandTypeLine { get; }
        public string Lexeme { get; }
        public int Position { get; }

        public OperandTypeNotDefinedException(int position, string lexeme, string operandTypeLine)
        {
            OperandTypeLine = operandTypeLine;
            Lexeme = lexeme;
            Position = position;
        }
    }
}
