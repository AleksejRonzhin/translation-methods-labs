using System.Text;

namespace library.tokens
{
    internal class UnderBracketsExpression : TokenInfo
    {
        private readonly List<TokenInfo> _tokens;

        public UnderBracketsExpression(List<TokenInfo> tokens) : base(new UnderBracketsExpressionFictToken(), tokens.First().Position, "фиктивный токен подскобочного выражения")
        {
            _tokens = tokens;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new("<>");
            _tokens.ForEach(token => stringBuilder.Append(token).Append('\n'));
            stringBuilder.Append("<end>");
            return stringBuilder.ToString();
        }

        public List<TokenInfo> GetTokens()
        {
            return _tokens;
        }

        private class UnderBracketsExpressionFictToken : OperandToken
        {
            public UnderBracketsExpressionFictToken() : base("")
            {
            }
        }
    }
}
