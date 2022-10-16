namespace library.tokens
{
    public class Token
    {
        public Token(int position, string tokenName, string text, TokenType type, int? attributeValue = null)
        {
            Position = position;
            TokenName = tokenName;
            AttributeValue = attributeValue;
            Text = text;
            Type = type;
        }

        public string TokenName { get; }
        public int? AttributeValue { get; }
        public string Text { get; }
        public TokenType Type { get; }

        public int Position { get; }

        public override string ToString()
        {
            return (Type == TokenType.IDENTIFIER_TOKEN) ? $"<id, {AttributeValue}> - {Text}" : $"<{TokenName}> - {Text}";
        }
    }
}
