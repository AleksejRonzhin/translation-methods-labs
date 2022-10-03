﻿using library.parameters;
using library.parameters.mappers;
using library.parameters.validators;

namespace lab2.parameters
{
    internal class SymbolsTableFilenameParameter : Parameter<string>
    {
        public SymbolsTableFilenameParameter(string arg) : base(arg, new EmptyMapper<string>(), new EmptyValidator<string>())
        {
        }
    }
}