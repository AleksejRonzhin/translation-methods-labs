namespace library.compiler.core.tokens
{
    public class TokenInfo
    {
        public static readonly TokenInfo Empty = new(Token.Empty, -1, "");
        public TokenInfo(Token token, int position, string text)
        {
            Position = position;
            Text = text;
            Token = token;
        }

        public Token Token { get; }
        public string Text { get; }

        public int Position { get; }

        public override string ToString()
        {
            return Token + " - " + Text;
        }
    }
}
