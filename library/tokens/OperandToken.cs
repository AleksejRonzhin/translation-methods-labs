namespace library.tokens
{
    public abstract class OperandToken : Token
    {
        protected OperandToken(string tokenName, int? attributeValue = null) : base(tokenName, attributeValue)
        {
        }
    }
}
