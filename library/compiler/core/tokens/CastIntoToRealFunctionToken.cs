using library.compiler.core.operations;

namespace library.compiler.core.tokens
{
    public class CastIntToRealFunctionToken : FunctionToken
    {
        public CastIntToRealFunctionToken() : base("IntToRealCast", OperandType.REAL)
        {
        }
    }
}
