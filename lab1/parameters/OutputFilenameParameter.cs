using library.parameters;
using library.parameters.mappers;
using library.parameters.validators;

namespace lab1.parameters
{
    internal class OutputFilenameParameter : Parameter<string>
    {
        public OutputFilenameParameter(string arg) : base(arg, new EmptyMapper<string>(), new EmptyValidator<string>())
        {
        }
    }
}