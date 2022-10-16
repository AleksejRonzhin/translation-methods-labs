namespace library.tokens.creators
{
    internal class CurlyBracesTokenCreator : TokenCreator
    {
        public CurlyBracesTokenCreator() : base(new CurlyBracesTokenChecker())
        {
        }

        public override Token GetToken()
        {
            string tokenName = this.tokenNameBuilder.ToString();
            string text = (tokenName == "(") ? "открыващая скобка" : "закрывающая скобка";
            return new Token(tokenName, text, TokenType.CURLY_BRACES_TOKEN);
        }

        private class CurlyBracesTokenChecker : TokenChecker
        {

            public CurlyBracesTokenChecker() : base(
                (symbol) => symbol == ')' || symbol == '(',
                (symbol) => false
                )
            {
            }
        }
    }
}
