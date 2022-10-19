using library.operations;

namespace library.tokens
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
