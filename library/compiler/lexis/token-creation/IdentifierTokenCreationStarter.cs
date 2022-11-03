using library.compiler.core.operations;
using library.compiler.core.symbols;
using library.compiler.core.symbols.exceptions;
using library.compiler.core.tokens;
using library.compiler.lexis.exceptions;
using library.tokens.creation.exceptions;

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
                try
                {
                    int attributeValue = symbolsTable.GetOrAddSymbol(tokenName, operandType);
                    return CreateTokenInfo(new IdentifierToken(tokenName, attributeValue), text);
                } catch(TokenAlreadyDefinedWithAnotherTypeException ex)
                {
                    throw new ConflictIdentifierTypesException(startPosition, tokenName, ex.OperandType, operandType);
                }
            }

            private (string lexeme, OperandType type) DefineType(string lexeme)
            {
                int openBracketCount = 0;
                int closeBracketCount = 0;
                for (int i = 0; i < lexeme.Length; i++)
                {
                    if (lexeme[i] == '[') openBracketCount++;
                    if (lexeme[i] == ']') closeBracketCount++;
                }
                if (openBracketCount == 0 && closeBracketCount == 0) return (lexeme, OperandType.ANY);

                if (openBracketCount != 1 || closeBracketCount != 1) throw new InvalidIdentifierException(lexeme, startPosition);
                int openBracketIndex = lexeme.IndexOf('[');
                int closeBracketIndex = lexeme.IndexOf(']');
                if (closeBracketIndex != lexeme.Length - 1) throw new InvalidIdentifierException(lexeme, startPosition);
                if (openBracketIndex > closeBracketIndex) throw new InvalidIdentifierException(lexeme, startPosition);

                string typeLine = lexeme[(openBracketIndex + 1)..closeBracketIndex];
                var operandType = OperandTypeUtils.GetByLine(typeLine);
                if (operandType == OperandType.NOT_DEFINED) throw new OperandTypeNotDefinedException(startPosition, lexeme, typeLine);

                return (lexeme[0..openBracketIndex], operandType);
            }
        }
    }
}
