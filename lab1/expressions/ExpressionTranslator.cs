namespace lab1.expressions
{
    internal class ExpressionTranslator : ExpressionCreator
    {
        private readonly List<string> sourceExpressions;

        public ExpressionTranslator(List<string> sourceExpressions, Dictionary<string, string> operations, Dictionary<int, string> operands)
            : base(operations, operands)
        {
            this.sourceExpressions = sourceExpressions;
        }

        public override List<string> CreateExpressions()
        {
            return this.sourceExpressions.ConvertAll(expression => TranslateExpression(expression));
        }

        private string TranslateExpression(string sourceExpression)
        {
            var expression = sourceExpression;
            expression = TranslateOperands(expression);
            expression = TranslateOperations(expression);
            return expression;
        }

        private string TranslateOperands(string sourceExpression)
        {
            var expression = sourceExpression;
            foreach (var (value, text) in this.operands)
            {
                expression = expression.Replace(value.ToString(), text);
            }
            return expression;
        }

        private string TranslateOperations(string sourceExpression)
        {
            var expression = sourceExpression;
            foreach (var (value, text) in this.operations)
            {
                expression = expression.Replace(value, text);
            }
            return expression;
        }
    }
}