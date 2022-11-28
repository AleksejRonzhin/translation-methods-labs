using library.compiler.core.models;

namespace library.compiler.core.tokens
{
    public class CastIntToRealFunctionToken : UnaryFunctionToken
    {
        public CastIntToRealFunctionToken() : base("i2r", "i2r", OperandType.INTEGER, OperandType.REAL)
        {
        }
    }
}