namespace library.tokens.creation
{
    internal class OperationTokenCreationStarter : TokenCreationStarter
    {
        public static readonly List<char> operationSymbols = new() { '+', '-', '*', '/', '=' };
        public static readonly Dictionary<string, (string, int)> operations = new(){
            {"+", ("операция сложения", 2) },
            {"-", ("операция вычитания", 2)},
            {"*", ("операция умножения", 1)},
            {"/", ("операция деления", 1)},
            {"=", ("операция присваивания", 3) }
        };
        private static readonly Predicate<char> startPredicate = (symbol) => operationSymbols.Contains(symbol);
        private static readonly Predicate<char> predicate = (symbol) => false;


        public OperationTokenCreationStarter() 
            : base(startPredicate)
        {
        }

        public override TokenCreationProcess Start(char startSymbol, int startPosition)
        {
            return new OperationTokenCreationProcess(startSymbol, startPosition);
        }

        private class OperationTokenCreationProcess : TokenCreationProcess
        {
            public OperationTokenCreationProcess(char startSymbol, int startPosition) 
                : base(predicate, startSymbol, startPosition)
            {
            }

            public override TokenInfo Finish()
            {
                string lexeme = GetLexeme();
                (string text, int prioritet) = operations[lexeme];
                return CreateTokenInfo(new OperationToken(lexeme, prioritet), text);
            }
        }
    }
}
