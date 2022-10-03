using System.Text;

namespace lab2.tokens.creators
{
    abstract class TokenCreator
    {
        public TokenCreator(TokenChecker tokenChecker)
        {
            TokenChecker = tokenChecker;
        }

        public TokenChecker TokenChecker { get; }
        protected StringBuilder tokenNameBuilder = new();

        public bool Start(char startSymbol)
        {
            if (!TokenChecker.StartSymbolPredicate(startSymbol)) return false;
            tokenNameBuilder = new();
            tokenNameBuilder.Append(startSymbol);
            return true;
        }

        public bool AddSymbol(char symbol)
        {
            if (!TokenChecker.SymbolPredicate(symbol)) return false;
            tokenNameBuilder.Append(symbol);
            return true;
        }

        abstract public Token GetToken();
    }
}
