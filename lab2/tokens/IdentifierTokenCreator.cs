namespace lab2.tokens
{
    internal class IdentifierTokenCreator : TokenCreator
    {
        public IdentifierTokenCreator() : base(new IdentifierTokenType())
        {
        }

        private class IdentifierTokenType : TokenType
        {
            public IdentifierTokenType() : base(
                (symbol) => symbol >= 'a' && symbol <= 'z',
                (symbol) => symbol >= 'a' && symbol <= 'z' || symbol == '_' || symbol >= '0' && symbol <= '9',
                true)
            {
            }
        }
    }
}
