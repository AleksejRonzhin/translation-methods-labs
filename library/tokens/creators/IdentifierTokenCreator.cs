namespace library.tokens.creators
{
    internal class IdentifierTokenCreator : TokenCreator
    {
        private SymbolsTable _symbolsTable;

        public IdentifierTokenCreator(SymbolsTable symbolsTable) : base(new IdentifierTokenChecker())
        {
            _symbolsTable = symbolsTable;
        }

        public override TokenInfo GetToken()
        {
            string tokenName = this.tokenNameBuilder.ToString();
            string text = $"идентификатор с именем {tokenName}";
            int attributeValue = _symbolsTable.Add(tokenName);
            return Create(new IdentifierToken(tokenName, attributeValue), text);
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
