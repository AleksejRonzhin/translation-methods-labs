namespace library.compiler.core.models
{
    public class ThreeAddressLine
    {
        private readonly string _operationCode;
        private readonly string _result;
        private readonly string _operand1;
        private readonly string? _operand2;

        public string Result { get
            {
                return _result;
            } 
        }

        public ThreeAddressLine(string operationCode, string result, string operand1, string? operand2 = null)
        {
            _operationCode = operationCode;
            _result = result;
            _operand1 = operand1;
            _operand2 = operand2;
        }

        public override string ToString()
        {
            return $"{_operationCode} {_result} {_operand1} {_operand2 ?? ""}";
        }
    }
}
