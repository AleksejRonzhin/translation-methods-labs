using System.Text;

namespace library.tokens.creation
{
    internal abstract class TokenCreationProcess
    {
        private readonly Predicate<char> predicate;
        private readonly StringBuilder lexemeBuilder;
        protected readonly int startPosition;

        internal TokenCreationProcess(Predicate<char> predicate, char startSymbol, int startPosition)
        {
            this.predicate = predicate;
            this.lexemeBuilder = new StringBuilder();
            lexemeBuilder.Append(startSymbol);
            this.startPosition = startPosition;
        }

        public bool AddSymbol(char symbol)
        {
            if (!predicate.Invoke(symbol)) return false;
            lexemeBuilder.Append(symbol);
            return true;
        }

        protected string GetLexeme()
        {
            return lexemeBuilder.ToString();
        }

        protected TokenInfo CreateTokenInfo(Token token, string text)
        {
            return new(token, startPosition, text);
        }

        public abstract TokenInfo Finish();
    }
}
