namespace lab2.tokens
{
    internal class Token
    {
        public Token(string tokenName, string text, TokenType type, int? attributeValue = null)
        {
            TokenName = tokenName;
            AttributeValue = attributeValue;
            Text = text;
            Type = type;
        }

        public string TokenName { get; }
        public int? AttributeValue { get; }
        public string Text { get; }
        public TokenType Type { get; }

        public override string ToString()
        {
            return (Type == TokenType.IDENTIFIER_TOKEN) ? $"<id, {AttributeValue}> - {Text}" : $"<{TokenName}> - {Text}";
        }
    }
}
