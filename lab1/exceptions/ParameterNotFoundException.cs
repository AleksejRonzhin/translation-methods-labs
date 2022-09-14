namespace lab1.exceptions
{
    internal class ParameterNotFoundException : Exception
    {
        public string ParameterName { get; }
        public int ParameterNumber { get; }
        
        public ParameterNotFoundException(string parameterName, int parameterNumber)
        {
            ParameterName = parameterName;
            ParameterNumber = parameterNumber;
        }
    }
}