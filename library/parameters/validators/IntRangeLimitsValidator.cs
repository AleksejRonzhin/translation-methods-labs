using library.exceptions;

namespace library.parameters.validators
{
    public class IntRangeLimitsValidator : IValidateble<(int min, int max)>
    {
        public IntRangeLimitsValidator()
        {
        }

        public void Validate((int min, int max) range)
        {
            if (range.min > range.max) 
                throw new ValidationException($"Минимальное значение диапазона ({range.min}) больше максимального ({range.max}).");
        }
    }
}
