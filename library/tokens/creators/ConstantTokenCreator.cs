using library.tokens.exceptions;

namespace library.tokens.creators
{
    internal class ConstantTokenCreator : TokenCreator
    {
        public ConstantTokenCreator() : base(new ConstantTokenChecker())
        {
        }

        private class ConstantTokenChecker : TokenChecker
        {
            public ConstantTokenChecker() : base(
                (symbol) => symbol >= '0' && symbol <= '9',
                (symbol) => symbol >= '0' && symbol <= '9' || symbol == '.'
                )
            {
            }
        }

        public override TokenInfo GetToken()
        {
            var tokenName = this.tokenNameBuilder.ToString();
            var pointCount = tokenName.Split('.').Length - 1;
            if (pointCount > 1) throw new InvalidConstantException(tokenName);
            var text = (pointCount == 1) ? "константа вещественного числа" : "константа целого числа";
            return Create(new ConstantToken(tokenName), text);
        }
    }
}
