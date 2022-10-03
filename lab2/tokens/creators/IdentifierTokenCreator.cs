namespace lab2.tokens.creators
{
    internal class IdentifierTokenCreator : TokenCreator
    {
        private SymbolsTable _symbolsTable;

        public IdentifierTokenCreator(SymbolsTable symbolsTable) : base(new IdentifierTokenChecker())
        {
            _symbolsTable = symbolsTable;
        }

        public override Token GetToken()
        {
            string tokenName = this.tokenNameBuilder.ToString();
            string text = $"идентификатор с именем {tokenName}";
            int attributeValue = _symbolsTable.Add(tokenName);
            return new Token(tokenName, text, TokenType.IDENTIFIER_TOKEN, attributeValue);
        }

        private class IdentifierTokenChecker : TokenChecker
        {
            public IdentifierTokenChecker() : base(
                (symbol) => symbol >= 'a' && symbol <= 'z',
                (symbol) => symbol >= 'a' && symbol <= 'z' || symbol == '_' || symbol >= '0' && symbol <= '9'
                )
            {
            }
        }
    }
}
