using library.parameters;
using library.parameters.mappers;
using library.parameters.validators;

namespace lab2.parameters
{
    internal class TokensFilenameParameter : Parameter<string>
    {
        public TokensFilenameParameter(string arg) : base(arg, new EmptyMapper<string>(), new EmptyValidator<string>())
        {
        }
    }
}