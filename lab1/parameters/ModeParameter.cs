using library.exceptions;
using library.parameters;
using library.parameters.validators;
using library.parameters.mappers;

namespace lab1.parameters
{
    internal class ModeParameter : Parameter<string>
    {
        public ModeParameter(string arg)
            : base(arg, new EmptyMapper<string>(), new ValuesContainValueValidator<string>("T", "t", "G", "g"))
        {
        }

        public override string GetValue()
        {
            try
            {
                return base.GetValue();
            }
            catch (ValidationException ex)
            {
                throw new ValidationException("Режим программа задан не верно.\n" + ex.ValidationMessage);
            }
        }
    }
}
