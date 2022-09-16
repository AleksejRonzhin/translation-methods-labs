using library.exceptions;
using library.parameters;
using library.parameters.mappers;
using library.parameters.validators;

namespace lab1.parameters
{
    internal class MinOperandsParameter : Parameter<int>
    {
        public MinOperandsParameter(string arg) : base(arg, new StringToIntMapper(), new IntValueInRangeValidator(0, 20))
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
