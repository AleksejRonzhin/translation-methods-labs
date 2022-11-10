using library.compiler.core.models;

namespace library.compiler.core.tokens
{
    public class CastIntToRealFunctionToken : UnaryFunctionToken
    {
        public CastIntToRealFunctionToken() : base("IntToRealCast", "i2r", OperandType.INTEGER, OperandType.REAL)
        {
        }
    }
}