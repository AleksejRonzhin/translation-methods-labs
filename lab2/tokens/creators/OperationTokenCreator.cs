namespace lab2.tokens.creators
{
    internal class OperationTokenCreator : TokenCreator
    {
        public static readonly List<char> operationSymbols = new() { '+', '-', '*', '/' };
        public static readonly Dictionary<string, string> operations = new(){
            {"+", "операция сложения" },
            {"-", "операция вычитания"},
            {"*", "операция умножение"},
            {"/", "операция деления"}
        };

        public OperationTokenCreator() : base(new OperationTokenChecker())
        {
        }

        public override Token GetToken()
        {
            string tokenName = this.tokenNameBuilder.ToString();
            string text = operations[tokenName];
            return new Token(tokenName, text, TokenType.OPERATION_TOKEN);
        }

        private class OperationTokenChecker : TokenChecker
        {

            public OperationTokenChecker() : base(
                (symbol) => operationSymbols.Contains(symbol),
                (symbol) => false)
            {
            }
        }
    }
}
