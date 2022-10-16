namespace library.tokens
{
    public class TokenChecker
    {
        public Predicate<char> StartSymbolPredicate { get; }
        public Predicate<char> SymbolPredicate { get; }

        public TokenChecker(Predicate<char> startSymbolPredicate, Predicate<char> symbolPredicate)
        {
            StartSymbolPredicate = startSymbolPredicate;
            SymbolPredicate = symbolPredicate;
        }
    }
}
