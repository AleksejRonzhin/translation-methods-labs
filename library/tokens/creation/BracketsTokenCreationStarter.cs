namespace library.tokens.creation
{
    internal class BracketsTokenCreationStarter : TokenCreationStarter
    {
        private static readonly Predicate<char> startPredicate = (symbol) => symbol == ')' || symbol == '(';
        private static readonly Predicate<char> predicate = (symbol) => false;

        public BracketsTokenCreationStarter() : base(startPredicate)
        {
        }

        public override TokenCreationProcess Start(char startSymbol, int startPosition)
        {
            return new BracketsTokenCreationProcess(startSymbol, startPosition);
        }

        private class BracketsTokenCreationProcess : TokenCreationProcess
        {
            public BracketsTokenCreationProcess(char startSymbol, int startPosition) 
                : base(predicate, startSymbol, startPosition)
            {
            }

            public override TokenInfo Finish()
            {
                string lexeme = GetLexeme();
                string text = (lexeme == "(") ? "открыващая скобка" : "закрывающая скобка";
                return CreateTokenInfo(new BracketToken(lexeme), text);
            }
        }
    }
}
