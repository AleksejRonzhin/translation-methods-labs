using lab1.exceptions;
using System.Text;

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
            if (sourceExpression == "") return "";
            var parts = sourceExpression.Split();
            bool nextOperand = true;
            var result = new StringBuilder();
            foreach (string part in parts)
            {
                string translatedPart;
                if (nextOperand)
                {
                    try
                    {
                        translatedPart = TranslateOperand(int.Parse(part));
                    }
                    catch (FormatException)
                    {
                        throw new OperandNotIntegerException(part);
                    }
                }
                else
                {
                    translatedPart = TranslateOperation(part);
                }
                result.Append(translatedPart).Append(' ');
                nextOperand = !nextOperand;
            }
            return result.ToString();
        }

        private string TranslateOperand(int operand)
        {
            try
            {
                return this._operands[operand];
            }
            catch (KeyNotFoundException)
            {
                throw new OperandNotFoundException(operand);
            }
        }

        private string TranslateOperation(string operation)
        {
            try
            {
                return this._operations[operation];
            }
            catch (KeyNotFoundException)
            {
                throw new OperationNotFoundException(operation);
            }
        }
    }
}