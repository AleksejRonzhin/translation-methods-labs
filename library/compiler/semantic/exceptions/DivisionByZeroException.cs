using library.compiler.core.tokens;

namespace library.compiler.semantic.exceptions
{
    internal class DivisionByZeroException : Exception
    {
        public TokenInfo TokenInfo { get; }

        public DivisionByZeroException(TokenInfo tokenInfo)
        {
            TokenInfo = tokenInfo;
        }
    }
}