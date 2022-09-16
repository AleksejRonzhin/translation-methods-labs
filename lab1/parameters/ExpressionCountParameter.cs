using library.exceptions;
using library.parameters;
using library.parameters.validators;
using library.parameters.mappers;

namespace lab1.parameters
{
    internal class ExpressionCountParameter : Parameter<int>
    {
        public ExpressionCountParameter(string arg) : base(arg, new StringToIntMapper(), new IntValueInRangeValidator(0, 50))
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
                throw new ValidationException("Количество выражений задано не верно. " + ex.ValidationMessage);
            }
        }
    }
}
