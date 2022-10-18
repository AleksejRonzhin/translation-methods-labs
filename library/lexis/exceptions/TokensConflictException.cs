using library.tokens;

namespace library.lexis.exceptions
{
    internal class TokensConflictException : Exception
    {
        public TokensConflictException(TokenInfo firstToken, TokenInfo secondToken)
        {
            FirstToken = firstToken;
            SecondToken = secondToken;
        }

        public TokenInfo FirstToken { get; }
        public TokenInfo SecondToken { get; }
    }
}