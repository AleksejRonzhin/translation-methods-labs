using library.tokens;

namespace library.lexis.creators
{
    internal class OperationTokenCreator : TokenCreator
    {
        public static readonly List<char> operationSymbols = new() { '+', '-', '*', '/', '=' };
        public static readonly Dictionary<string, string> operations = new(){
            {"+", "операция сложения" },
            {"-", "операция вычитания"},
            {"*", "операция умножения"},
            {"/", "операция деления"},
            {"=", "операция присваивания" }
        };

        public OperationTokenCreator() : base(new OperationTokenChecker())
        {
        }

        public override TokenInfo GetToken()
        {
            string tokenName = this.tokenNameBuilder.ToString();
            string text = operations[tokenName];
            return Create(new OperationToken(tokenName), text);
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
