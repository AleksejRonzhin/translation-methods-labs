﻿namespace library.tokens.creators
{
    internal class OperationTokenCreator : TokenCreator
    {
        public static readonly List<char> operationSymbols = new() { '+', '-', '*', '/', '=' };
        public static readonly Dictionary<string, string> operations = new(){
            {"+", "операция сложения" },
            {"-", "операция вычитания"},
            {"*", "операция умножение"},
            {"/", "операция деления"},
            {"=", "операция присваивания" }
        };

        public OperationTokenCreator() : base(new OperationTokenChecker())
        {
        }

        public override Token GetToken()
        {
            string tokenName = this.tokenNameBuilder.ToString();
            string text = operations[tokenName];
            return CreateToken(tokenName, text, TokenType.OPERATION_TOKEN);
        }

        private class OperationTokenChecker : TokenChecker
        {

            public OperationTokenChecker() : base(
                (symbol) => operationSymbols.Contains(symbol),
                (symbol) => false)
            {
            }
        }
    }
}
