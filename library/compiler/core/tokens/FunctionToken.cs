using library.compiler.core.operations;

namespace library.compiler.core.tokens
{
    public class FunctionToken : OperandToken
    {
        public OperandType ResultType { get; }

        public FunctionToken(string tokenName, OperandType resultType) : base(tokenName)
        {            ResultType = resultType;
        }
    }
}
