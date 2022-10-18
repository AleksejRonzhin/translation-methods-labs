﻿using library.tokens;

namespace library.syntax.exceptions
{
    internal class OperandNotFoundException : Exception
    {
        public OperationToken OperationToken { get; }
        public int Position { get; }

        public OperandNotFoundException(OperationToken operationToken, int position)
        {
            OperationToken = operationToken;
            Position = position;
        }
    }
}
