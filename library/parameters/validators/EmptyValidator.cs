namespace library.parameters.validators
{
    public class EmptyValidator<T> : IValidateble<T>
    {
        public void Validate(T value)
        {
        }
    }
}
