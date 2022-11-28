using library.compiler.core.tokens;

namespace library.compiler.core.models
{
    [Serializable]
    public class ThreeAddressLine
    {
        private readonly string _operationCode;
        private OperandToken _result;
        private OperandToken _operand1;
        private OperandToken? _operand2;

        public OperandToken Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public OperandToken FirstOperand
        {
            get { return _operand1; }
            set { _operand1 = value; }
        }

        public OperandToken? SecondOperand
        {
            get { return _operand2; }
            set { _operand2 = value; }
        }

        public ThreeAddressLine(string operationCode, OperandToken result, OperandToken operand1, OperandToken? operand2 = null)
        {
            _operationCode = operationCode;
            _result = result;
            _operand1 = operand1;
            _operand2 = operand2;
        }

        public override string ToString()
        {
            var operand2Text = (_operand2 == null) ? "" : _operand2.ToString();
            return $"{_operationCode} {_result} {_operand1} {operand2Text}";
        }
    }
}
