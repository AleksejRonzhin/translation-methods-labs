using System.Text;

namespace lab2.tokens
{
    abstract class TokenCreator
    {
        public TokenCreator(TokenType tokenType)
        {
            TokenType = tokenType;
        }

        public TokenType TokenType { get; }
        private StringBuilder tokenNameBuilder = new();

        public bool Start(char startSymbol)
        {
            if (!TokenType.StartSymbolPredicate(startSymbol)) return false;
            tokenNameBuilder = new();
            tokenNameBuilder.Append(startSymbol);
            return true;
        }

        public bool AddSymbol(char symbol)
        {
            if (!TokenType.SymbolPredicate(symbol)) return false;
            tokenNameBuilder.Append(symbol);
            return true;
        }

        public string GetToken()
        {
            return tokenNameBuilder.ToString();
        }
    }
}
