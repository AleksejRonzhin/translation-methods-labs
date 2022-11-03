namespace library.tokens
{
    public class TokenInfo
    {
        public static TokenInfo Empty = new(Token.Empty, -1, "");
        public TokenInfo(Token token, int position, string text)
        {
            Position = position;
            Text = text;
            Token = token;
        }

        public Token Token { get; }
        public string Text { get; }

        public int Position { get; }

        public override string ToString()
        {
            return Token + " - " + Text;
            //return (Type == TokenType.IDENTIFIER_TOKEN) ? $"<id, {AttributeValue}> - {Text}" : $"<{TokenName}> - {Text}";
        }
    }
}
