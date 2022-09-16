namespace lab1.expressions
{
    internal class ExpressionTranslator : ExpressionCreator
    {
        private readonly List<string> _sourceExpressions;

        public ExpressionTranslator(List<string> sourceExpressions, Dictionary<string, string> operations, Dictionary<int, string> operands)
            : base(operations, operands)
        {
            this._sourceExpressions = sourceExpressions;
        }

        public override List<string> CreateExpressions()
        {
            return this._sourceExpressions.ConvertAll(expression => TranslateExpression(expression));
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
            foreach (var (value, text) in this._operands)
            {
                expression = expression.Replace(value.ToString(), text);
            }
            return expression;
        }

        private string TranslateOperations(string sourceExpression)
        {
            var expression = sourceExpression;
            foreach (var (value, text) in this._operations)
            {
                expression = expression.Replace(value, text);
            }
            return expression;
        }
    }
}