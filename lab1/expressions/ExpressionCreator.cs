namespace lab1.expressions
{
    internal abstract class ExpressionCreator
    {
        protected readonly Dictionary<string, string> operations;
        protected readonly Dictionary<int, string> operands;

        protected ExpressionCreator(Dictionary<string, string> operations, Dictionary<int, string> operands)
        {
            this.operations = operations;
            this.operands = operands;
        }

        public abstract List<string> CreateExpressions();
    }
}