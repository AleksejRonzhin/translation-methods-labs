using library.compiler.core.models;
using System.Globalization;

namespace library.compiler.core.tokens
{
    public class ConstantToken : OperandToken
    {
        public readonly static NumberFormatInfo numberFormatInfo = new()
        {
            NumberDecimalSeparator = "."
        };


        public ConstantToken(string tokenName) : base(tokenName)
        {
        }

        public OperandType GetOperandType()
        {
            var substrings = TokenName.Split(numberFormatInfo.NumberDecimalSeparator);
            if (substrings.Length == 1)
                return OperandType.INTEGER;
            return OperandType.REAL;
        }
    }
}