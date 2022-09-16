using library.exceptions;

namespace library.parameters.validators
{
    public class ValuesContainValueValidator<T> : IValidateble<T>
    {
        private readonly T[] _values;

        public ValuesContainValueValidator(params T[] values)
        {
            _values = values;
        }

        public void Validate(T value)
        {
            if (!_values.Contains(value))
                throw new ValidationException(
                    $"Значение {value} является не допустимым. Допустимые значения: {string.Join(", ", _values)}."
                    );
        }
    }
}