using library.tokens;

namespace library.lexis.creators
{
    internal class OperationTokenCreator : TokenCreator
    {
        public static readonly List<char> operationSymbols = new() { '+', '-', '*', '/', '=' };
        public static readonly Dictionary<string, (string, int)> operations = new(){
            {"+", ("операция сложения", 2) },
            {"-", ("операция вычитания", 2)},
            {"*", ("операция умножения", 1)},
            {"/", ("операция деления", 1)},
            {"=", ("операция присваивания", 3) }
        };

        public OperationTokenCreator() : base(new OperationTokenChecker())
        {
        }

        public override TokenInfo GetToken()
        {
            string tokenName = this.tokenNameBuilder.ToString();
            (string text, int prioritet) = operations[tokenName];
            return Create(new OperationToken(tokenName, prioritet), text);
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
