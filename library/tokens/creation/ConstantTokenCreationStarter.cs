using library.lexis.exceptions;

namespace library.tokens.creation
{
    internal class ConstantTokenCreationStarter : TokenCreationStarter
    {
        private static readonly Predicate<char> StartPredicate = (symbol) => symbol >= '0' && symbol <= '9';
        private static readonly Predicate<char> Predicate = (symbol) => symbol >= '0' && symbol <= '9' || symbol == '.';

        public ConstantTokenCreationStarter()
            : base(StartPredicate)
        {
        }

        public override TokenCreationProcess Start(char startSymbol, int startPosition)
        {
            return new ConstantTokenCreationProcess(startSymbol, startPosition);
        }

        private class ConstantTokenCreationProcess : TokenCreationProcess
        {
            public ConstantTokenCreationProcess(char startSymbol, int startPosition)
                : base(Predicate, startSymbol, startPosition)
            {
            }

            public override TokenInfo Finish()
            {
                var lexeme = GetLexeme();
                var pointCount = lexeme.Split('.').Length - 1;
                if (pointCount > 1) throw new InvalidConstantException(lexeme, startPosition);
                var text = (pointCount == 1) ? "константа вещественного числа" : "константа целого числа";
                return CreateTokenInfo(new ConstantToken(lexeme), text);
            }
        }
    }
}
