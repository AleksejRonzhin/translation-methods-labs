namespace library.tokens
{
    public abstract class Token
    {
        public string TokenName { get; }
        public int? AttributeValue { get; }

        public Token(string tokenName, int? attributeValue = null)
        {
            TokenName = tokenName;
            AttributeValue = attributeValue;
        }

        public override string ToString()
        {
            return (AttributeValue == null)? $"<{TokenName}>" : $"<id, {AttributeValue}>";
        }
    }
}