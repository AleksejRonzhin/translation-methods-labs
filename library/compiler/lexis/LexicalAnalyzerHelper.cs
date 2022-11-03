using library.compiler.core.symbols;
using library.compiler.core.tokens;
using library.compiler.lexis.exceptions;
using library.tokens.creation;

namespace library.compiler.lexis
{
    internal class LexicalAnalyzerHelper
    {
        private readonly SymbolsTable symbolsTable;
        private readonly List<TokenInfo> tokens = new();
        private TokenInfo? prevTokenInfo = null;

        public LexicalAnalyzerHelper(SymbolsTable symbolsTable)
        {
            this.symbolsTable = symbolsTable;
        }


        internal TokenCreationStarter? TryGetTokenCreationStarter(char symbol)
        {
            List<TokenCreationStarter> startes = new()
            {
                new ConstantTokenCreationStarter(),
                new BracketsTokenCreationStarter(),
                new IdentifierTokenCreationStarter(symbolsTable),
                new OperationTokenCreationStarter()
            };

            foreach (var starter in startes)
            {
                if (starter.CanStart(symbol)) return starter;
            }

            return null;
        }

        internal void AddToken(TokenInfo token)
        {
            CheckTokensConflict(token);
            tokens.Add(token);
            prevTokenInfo = token;
        }

        private void CheckTokensConflict(TokenInfo tokenInfo)
        {
            if (prevTokenInfo == null) return;
            if (tokenInfo.Token is IdentifierToken && prevTokenInfo.Token is ConstantToken)
                throw new TokensConflictException(prevTokenInfo, tokenInfo, tokenInfo.Position);
        }

        internal void RefreshPrevTokenInfo()
        {
            prevTokenInfo = null;
        }

        internal List<TokenInfo> GetTokens()
        {
            return tokens;
        }
    }
}
