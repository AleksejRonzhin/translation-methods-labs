namespace library.tokens
{
    public class ConstantToken : OperandToken
    {
        public ConstantToken(string tokenName) : base(tokenName)
        {
        }

        public OperandType GetOperandType()
        {
            var substrings = TokenName.Split(".");
            if (substrings.Length == 1)
                return OperandType.INTEGER;
            return OperandType.REAL;
        }
    }
}