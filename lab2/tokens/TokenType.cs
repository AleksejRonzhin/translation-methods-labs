namespace lab2.tokens
{
    internal class TokenType
    {
        public Predicate<char> StartSymbolPredicate { get; }
        public Predicate<char> SymbolPredicate { get; }
        public bool NeedAttributeValue;

        public TokenType(Predicate<char> startSymbolPredicate, Predicate<char> symbolPredicate, bool needAttributeValue = false)
        {
            StartSymbolPredicate = startSymbolPredicate;
            SymbolPredicate = symbolPredicate;
            NeedAttributeValue = needAttributeValue;
        }
    }
}
