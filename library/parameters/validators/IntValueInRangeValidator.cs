using library.exceptions;

namespace library.parameters.validators
{
    public class IntValueInRangeValidator : IValidateble<int>
    {
        private (int min, int max) _range;

        public IntValueInRangeValidator(int minValue, int maxValue)
        {
            _range = (minValue, maxValue);
        }

        public void Validate(int value)
        {
            if (value < _range.min || value > _range.max) 
                throw new ValidationException($"{value} не лежит в диапазоне [{_range.min}, {_range.max}].");
        }
    }
}
