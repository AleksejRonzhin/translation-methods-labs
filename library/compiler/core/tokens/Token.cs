namespace library.compiler.core.tokens
{
    public abstract class Token
    {
        public static readonly Token Empty = new EmptyToken();
        public string TokenName { get; }
        public int? AttributeValue { get; }

        public Token(string tokenName, int? attributeValue = null)
        {
            TokenName = tokenName;
            AttributeValue = attributeValue;
        }

        public override string ToString()
        {
            return AttributeValue == null ? $"<{TokenName}>" : $"<id, {AttributeValue}>";
        }

        private class EmptyToken : Token
        {
            public EmptyToken() : base("emptyToken")
            {
            }
        }
    }
}