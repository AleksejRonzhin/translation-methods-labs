namespace lab2.tokens
{
    internal class OperationTokenCreator : TokenCreator
    {
        public static readonly List<char> operations = new(){ '+', '-', '*', '/' };

        public OperationTokenCreator() : base(new OperationTokenType())
        {
        }

        private class OperationTokenType : TokenType
        {

            public OperationTokenType() : base(
                (symbol) => operations.Contains(symbol),
                (symbol) => false,
                true)
            {
            }
        }
    }
}
