namespace lab1.exceptions
{
    internal class ValidationException : Exception
    {
        public string ValidationMessage { get; }

        public ValidationException(string validationMessage)
        {
            this.ValidationMessage = validationMessage;
        }
    }
}
