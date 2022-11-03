using library.tokens;

namespace library.semantic.exceptions
{
    internal class DivisionByZeroException : Exception
    {
        public Token token;

        public DivisionByZeroException(Token token)
        {
            this.token = token;
        }
    }
}
