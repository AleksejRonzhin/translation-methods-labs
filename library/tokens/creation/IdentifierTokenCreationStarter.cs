namespace library.tokens.creation
{
    internal class IdentifierTokenCreationStarter : TokenCreationStarter
    {
        private static readonly Predicate<char> startPredicate = (symbol) => symbol >= 'a' && symbol <= 'z' || symbol == '_';
        private static readonly Predicate<char> predicate = (symbol) => symbol >= 'a' && symbol <= 'z' || symbol == '_' || symbol >= '0' && symbol <= '9';

        private readonly SymbolsTable symbolsTable;

        public IdentifierTokenCreationStarter(SymbolsTable symbolsTable) 
            : base(startPredicate)
        {
            this.symbolsTable = symbolsTable;
        }

        public override TokenCreationProcess Start(char startSymbol, int startPosition)
        {
            return new IdentifierTokenCreationProcess(startSymbol, startPosition, symbolsTable);
        }

        private class IdentifierTokenCreationProcess : TokenCreationProcess
        {
            private readonly SymbolsTable symbolsTable;

            public IdentifierTokenCreationProcess(char startSymbol, int startPosition, SymbolsTable symbolsTable)
                : base(predicate, startSymbol, startPosition)
            {
                this.symbolsTable = symbolsTable;
            }

            public override TokenInfo Finish()
            {
                string lexeme = GetLexeme();
                string text = $"идентификатор с именем {lexeme}";
                int attributeValue = symbolsTable.Add(lexeme);
                return CreateTokenInfo(new IdentifierToken(lexeme, attributeValue), text);
            }
        }
    }
}
