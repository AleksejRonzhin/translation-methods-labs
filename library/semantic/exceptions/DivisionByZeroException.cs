using library.tokens;

namespace library.semantic.exceptions
{
    internal class DivisionByZeroException : Exception
    {
        public TokenInfo TokenInfo { get; }

        public DivisionByZeroException(TokenInfo tokenInfo)
        {
            this.TokenInfo = tokenInfo;
        }
    }
}