namespace library.tokens.exceptions
{
    internal class TokensConflictException : Exception
    {
        public TokensConflictException(Token firstToken, Token secondToken)
        {
            FirstToken = firstToken;
            SecondToken = secondToken;
        }

        public Token FirstToken { get; }
        public Token SecondToken { get; }
    }
}