﻿using library.parameters;
using library.parameters.mappers;
using library.parameters.validators;

namespace compiler.parameters
{
    internal class SyntaxTreeFilenameParameter : Parameter<string>
    {
        public SyntaxTreeFilenameParameter(string arg) : base(arg, new EmptyMapper<string>(), new EmptyValidator<string>())
        {
        }
    }
}