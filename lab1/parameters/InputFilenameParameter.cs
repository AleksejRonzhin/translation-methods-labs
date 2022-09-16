using library.parameters;
using library.parameters.mappers;
using library.parameters.validators;

namespace lab1.parameters
{
    internal class InputFilenameParameter : Parameter<string>
    {
        public InputFilenameParameter(string arg) : base(arg, new EmptyMapper<string>(), new EmptyValidator<string>())
        {
        }
    }
}
