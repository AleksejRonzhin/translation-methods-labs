using System.Text;
using library.tokens;

namespace library.syntax
{
    internal class UnderBracketsExpression : TokenInfo
    {
        private readonly List<TokenInfo> _tokenInfos;

        public UnderBracketsExpression(List<TokenInfo> tokenInfos) 
            : base(new UnderBracketsExpressionFictToken(tokenInfos), tokenInfos.First().Position, "фиктивный токен подскобочного выражения")
        {
            _tokenInfos = tokenInfos;
        }

        public List<TokenInfo> GetTokens()
        {
            return _tokenInfos;
        }

        private class UnderBracketsExpressionFictToken : OperandToken
        {
            private List<TokenInfo> tokenInfos;

            public UnderBracketsExpressionFictToken(List<TokenInfo> tokenInfos) : base("")
            {
                this.tokenInfos = tokenInfos;
            }

            public override string ToString()
            {
                return new StringBuilder("(")
                    .Append(String.Join(" ", tokenInfos.Select(info => info.Token.TokenName)))
                    .Append(')').ToString();
            }
        }
    }
}
