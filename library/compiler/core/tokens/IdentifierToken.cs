namespace library.compiler.core.tokens
{
    public class IdentifierToken : OperandToken
    {
        public IdentifierToken(string tokenName, int attributeValue) : base(tokenName, attributeValue)
        {
        }
    }
}