using library.parameters;
using library.parameters.mappers;
using library.parameters.validators;

namespace compiler.parameters
{
    internal class TokensFilenameParameter : Parameter<string>
    {
        public TokensFilenameParameter(string arg) : base(arg, new EmptyMapper<string>(), new EmptyValidator<string>())
        {
        }
    }
}