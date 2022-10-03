﻿namespace lab2.exceptions
{
    internal class InvalidSymbolException : Exception
    {
        public InvalidSymbolException(char symbol)
        {
            Symbol = symbol;
        }

        public char Symbol { get; }
    }
}
