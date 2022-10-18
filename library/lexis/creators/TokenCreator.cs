using library.tokens;
using System.Text;

namespace library.lexis.creators
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

        protected TokenInfo Create(Token token, string text)
        {
            return new(token, _position, text);
        }

        abstract public TokenInfo GetToken();
    }
}
