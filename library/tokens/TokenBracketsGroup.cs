using System.Text;

namespace library.tokens
{
    internal class TokenBracketsGroup : Token
    {
        private List<Token> _tokens;

        public TokenBracketsGroup(List<Token> tokens) 
            : base(tokens.First().Position, "", "", TokenType.BRACKETS_TOKEN_GROUP)
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

        public List<Token> GetTokens()
        {
            return _tokens;
        }
    }
}
