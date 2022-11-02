using library.symbols;

namespace library.tokens.creation
{
    internal class IdentifierTokenCreationStarter : TokenCreationStarter
    {
        private static readonly Predicate<char> startPredicate = (symbol) => (symbol >= 'a' && symbol <= 'z')
        || symbol == '_' || (symbol >= 'A' && symbol <= 'Z');
        private static readonly Predicate<char> predicate = (symbol) => (symbol >= 'a' && symbol <= 'z')
        || symbol == '_' || (symbol >= 'A' && symbol <= 'Z') || (symbol >= '0' && symbol <= '9')
        || symbol == '[' || symbol == ']';

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
                (string tokenName, OperandType operandType) = DefineType(GetLexeme());

                string text = $"идентификатор с именем {tokenName}";
                int attributeValue = symbolsTable.GetOrAddSymbol(tokenName, operandType);
                return CreateTokenInfo(new IdentifierToken(tokenName, attributeValue), text);
            }

            private (string lexeme, OperandType type) DefineType(string lexeme)
            {
                int a = 0;
                int b = 0;
                for (int i = 0; i < lexeme.Length; i++)
                {
                    if (lexeme[i] == '[') a++;
                    if (lexeme[i] == ']') b++;
                }
                if (a == 0 && b == 0) return (lexeme, OperandType.ANY);

                if (a != 1 || b != 1) throw new Exception();
                int aIndex = lexeme.IndexOf('[');
                int bIndex = lexeme.IndexOf(']');
                if (bIndex != lexeme.Length - 1) throw new Exception();
                if (aIndex > bIndex) throw new Exception();

                string typeLine = lexeme[(aIndex + 1)..bIndex];
                return (lexeme[0..aIndex], OperandTypeUtils.GetByLine(typeLine));
            }
        }
    }
}
