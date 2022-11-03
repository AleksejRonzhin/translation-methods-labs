using library.compiler.core.operations;

namespace library.compiler.core.tokens
{
    public class OperationToken : Token
    {
        public Operation Operation { get; }

        public OperationToken(Operation operation) : base(operation.Sign)
        {
            Operation = operation;
        }
    }
}
