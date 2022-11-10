using library.compiler.core.models;

namespace library.compiler.core.tokens
{
    public class UnaryFunctionToken : OperandToken
    {
        public OperandType OperandType { get; }
        public OperandType ResultType { get; }

        public string Code { get; }

        public UnaryFunctionToken(string tokenName, string code, OperandType operandType, OperandType resultType) : base(tokenName)
        {            OperandType = operandType;            ResultType = resultType;
            Code = code;
        }
    }
}
