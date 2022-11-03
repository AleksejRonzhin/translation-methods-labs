using library.compiler.core.operations;
using library.compiler.core.tokens;
using library.tokens.creation.exceptions;

namespace library.tokens.creation
{
    internal class OperationTokenCreationStarter : TokenCreationStarter
    {
        private static readonly Predicate<char> startPredicate 
            = (symbol) => Operation.ValidSymbols.Contains(symbol);
        private static readonly Predicate<char> predicate 
            = (symbol) => Operation.ValidSymbols.Contains(symbol);


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
                var operation = Operation.Operations.Find(operation => operation.Sign == lexeme);
                if (operation == null) throw new OperationNotFoundException(lexeme, startPosition);
                return CreateTokenInfo(new OperationToken(operation), operation.Name);
            }
        }
    }
}
