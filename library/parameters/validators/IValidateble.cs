namespace library.parameters.validators
{
    public interface IValidateble<T>
    {
        void Validate(T value);
    }
}