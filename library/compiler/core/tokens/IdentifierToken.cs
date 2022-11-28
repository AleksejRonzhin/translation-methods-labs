namespace library.compiler.core.tokens
{
    [Serializable]
    public class IdentifierToken : OperandToken
    {
        public IdentifierToken(string tokenName, int attributeValue) : base(tokenName, attributeValue)
        {
        }
    }
}