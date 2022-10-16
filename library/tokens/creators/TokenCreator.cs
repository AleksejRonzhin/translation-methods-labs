using System.Text;

namespace library.tokens.creators
{
    abstract class TokenCreator
    {
        private int _position;

        public TokenCreator(TokenChecker tokenChecker)
        {
            TokenChecker = tokenChecker;
        }

        public TokenChecker TokenChecker { get; }
        protected StringBuilder tokenNameBuilder = new();

        public bool Start(char startSymbol, int position)
        {
            if (!TokenChecker.StartSymbolPredicate(startSymbol)) return false;
            tokenNameBuilder = new();
            tokenNameBuilder.Append(startSymbol);
            _position = position;
            return true;
        }

        public bool AddSymbol(char symbol)
        {
            if (!TokenChecker.SymbolPredicate(symbol)) return false;
            tokenNameBuilder.Append(symbol);
            return true;
        }

        protected Token CreateToken(string tokenName, string text, TokenType type, int? attributeValue = null)
        {
            return new Token(_position, tokenName, text, type, attributeValue);
        }

        abstract public Token GetToken();
    }
}
