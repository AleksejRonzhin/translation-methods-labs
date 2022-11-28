using library.parameters;
using library.parameters.exceptions;
using library.parameters.mappers;
using library.parameters.validators;

namespace compiler.parameters
{
    internal class ModeParameter : Parameter<string>
    {
        public ModeParameter(string arg)
           : base(arg, new EmptyMapper<string>(),
                 new ValuesContainValueValidator<string>("LEX", "lex", "SYN", "syn", "SEM", "sem",
                     "GEN1", "gen1", "GEN2", "gen2", "GEN1_OPT", "gen1_opt", "GEN2_OPT", "gen2_opt", "GEN3", "gen3"))
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
