namespace library.tokens.creators
{
    internal class CurlyBracesTokenCreator : TokenCreator
    {
        public CurlyBracesTokenCreator() : base(new CurlyBracesTokenChecker())
        {
        }

        public override TokenInfo GetToken()
        {
            string tokenName = this.tokenNameBuilder.ToString();
            string text = (tokenName == "(") ? "открыващая скобка" : "закрывающая скобка";
            return Create(new BracketToken(tokenName), text);
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
