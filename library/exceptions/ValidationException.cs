namespace library.exceptions
{
    public class ValidationException : Exception
    {
        public string ValidationMessage { get; }

        public ValidationException(string validationMessage)
        {
            this.ValidationMessage = validationMessage;
        }
    }
}
