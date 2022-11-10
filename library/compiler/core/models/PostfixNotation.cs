using library.compiler.core.tokens;

namespace library.compiler.core.models
{
    public class PostfixNotation
    {
        private readonly List<Token> tokens;

        public PostfixNotation(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public PostfixNotation(List<PostfixNotation> postfixNotations)
        {
            tokens = new();
            postfixNotations.ForEach(postfixNotation =>
            {
                tokens.AddRange(postfixNotation.tokens);
            });
        }

        public PostfixNotation(Token token)
        {
            tokens = new();
            tokens.Add(token);
        }

        public override string ToString()
        {
            return string.Join(" ", tokens);
        }
    }
}
