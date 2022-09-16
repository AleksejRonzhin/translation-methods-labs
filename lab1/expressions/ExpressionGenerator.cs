using System.Text;

namespace lab1.expressions
{
    internal class ExpressionGenerator : ExpressionCreator
    {
        private readonly int _expressionsCount;
        private readonly (int min, int max) _range;
        private readonly Random _random = new();

        public ExpressionGenerator(int expressionsCount, (int min, int max) range, Dictionary<string, string> operations, Dictionary<int, string> operands) :
            base(operations, operands)
        {
            this._expressionsCount = expressionsCount;
            this._range = range;
        }

        public override List<string> CreateExpressions()
        {
            var expressions = new List<string>();
            for (int i = 0; i < _expressionsCount; i++)
            {
                var operandCount = this._random.Next(this._range.min, this._range.max);
                var operands = GenerateOperands(operandCount);
                var expression = string.Join($" {GenerateOperation()} ", operands);
                expressions.Add(expression);
            }
            return expressions;
        }

        private ICollection<int> GenerateOperands(int count)
        {
            var operandValues = new List<int>(this._operands.Keys);
            var operands = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                operands.Add(GetRandomValueFromList(operandValues));
            }
            return operands;
        }

        private string GenerateOperation()
        {
            var operationValues = new List<string>(this._operations.Keys);
            return GetRandomValueFromList<string>(operationValues);
        }

        private T GetRandomValueFromList<T>(List<T> list)
        {
            var randomIndex = this._random.Next(list.Count);
            return list[randomIndex];
        }
    }
}