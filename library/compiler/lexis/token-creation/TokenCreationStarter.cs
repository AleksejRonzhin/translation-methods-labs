namespace library.tokens.creation
{
    internal abstract class TokenCreationStarter
    {
        private readonly Predicate<char> startPredicate;

        public TokenCreationStarter(Predicate<char> startPredicate)
        {
            this.startPredicate = startPredicate;
        }

        public bool CanStart(char symbol)
        {
            return startPredicate.Invoke(symbol);
        }

        public abstract TokenCreationProcess Start(char startSymbol, int startPosition);
    }
}