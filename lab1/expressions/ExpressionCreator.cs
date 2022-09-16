namespace lab1.expressions
{
    internal abstract class ExpressionCreator
    {
        protected readonly Dictionary<string, string> _operations;
        protected readonly Dictionary<int, string> _operands;

        protected ExpressionCreator(Dictionary<string, string> operations, Dictionary<int, string> operands)
        {
            this._operations = operations;
            this._operands = operands;
        }

        public abstract List<string> CreateExpressions();
    }
}