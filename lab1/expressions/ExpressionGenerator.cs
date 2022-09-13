using System.Text;

namespace lab1.expressions
{
    internal class ExpressionGenerator : ExpressionCreator
    {
        private readonly int expressionsCount;
        private readonly int minOperands;
        private readonly int maxOperands;
        private readonly Random random = new();

        public ExpressionGenerator(int expressionsCount, int minOperands, int maxOperands, Dictionary<string, string> operations, Dictionary<int, string> operands) :
            base(operations, operands)
        {
            this.expressionsCount = expressionsCount;
            this.minOperands = minOperands;
            this.maxOperands = maxOperands;
        }

        public override List<string> CreateExpressions()
        {
            var expressions = new List<string>();
            for (int i = 0; i < expressionsCount; i++)
            {
                var operandCount = this.random.Next(minOperands, maxOperands);
                var operands = GenerateOperands(operandCount);
                var expression = string.Join($" {GenerateOperation()} ", operands);
                expressions.Add(expression);
            }
            return expressions;
        }

        private ICollection<int> GenerateOperands(int count)
        {
            var operandValues = new List<int>(this.operands.Keys);
            var operands = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                operands.Add(GetRandomValueFromList(operandValues));
            }
            return operands;
        }

        private string GenerateOperation()
        {
            var operationValues = new List<string>(this.operations.Keys);
            return GetRandomValueFromList<string>(operationValues);
        }

        private T GetRandomValueFromList<T>(List<T> list)
        {
            var randomIndex = random.Next(list.Count);
            return list[randomIndex];
        }
    }
}