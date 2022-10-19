namespace library.tokens
{
    public class OperationToken : Token
    {
        public int Prioritet { get; }

        public OperationToken(string tokenName, int prioritet) : base(tokenName)
        {
            Prioritet = prioritet;
        }
    }
}
