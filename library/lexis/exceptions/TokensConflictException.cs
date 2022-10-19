using library.tokens;

namespace library.lexis.exceptions
{
    internal class TokensConflictException : Exception
    {
        public TokensConflictException(TokenInfo firstToken, TokenInfo secondToken, int position)
        {
            FirstToken = firstToken;
            SecondToken = secondToken;
            Position = position;
        }

        public TokenInfo FirstToken { get; }
        public TokenInfo SecondToken { get; }

        public int Position { get; }
    }
}