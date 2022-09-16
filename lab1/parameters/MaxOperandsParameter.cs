using library.exceptions;
using library.parameters;
using library.parameters.validators;
using library.parameters.mappers;

namespace lab1.parameters
{
    internal class MaxOperandsParameter : Parameter<int>
    {
        public MaxOperandsParameter(string arg) : base(arg, new StringToIntMapper(), new IntValueInRangeValidator(0, 20))
        {
        }

        public override int GetValue()
        {
            try
            {
                return base.GetValue();
            }
            catch (ValidationException ex)
            {
                throw new ValidationException("Минимальное количество операндов задано не верно.\n" + ex.ValidationMessage);
            }
        }
    }
}
