using library.tokens;

namespace library.syntax
{
    public class SyntaxAnalyzer
    {
        public static SyntaxTree Analyze(List<Token> tokens)
        {
            var tokens2 = RemoveBrackets(tokens);
            tokens2.ForEach(token => Console.WriteLine(token));
            CheckErrors(tokens2);
            return CreateSyntaxTree(tokens2);
        }

        private static SyntaxTree CreateSyntaxTree(List<Token> tokens)
        {
            return null;
        }

        private static void CheckErrors(List<Token> tokens)
        {
            Console.WriteLine("Begin with " + tokens.First().TokenName);
            Console.WriteLine("Size " + tokens.Count);

            for (int i = 0; i < tokens.Count; i++)
            {
                TokenType currentTokenType = tokens[i].Type;
                TokenType? prevTokenType = (i - 1 == -1) ? null : tokens[i - 1].Type;
                TokenType? nextTokenType = (i + 1 >= tokens.Count) ? null : tokens[i + 1].Type;
                switch (currentTokenType)
                {
                    case TokenType.CONSTANT_TOKEN:
                    case TokenType.IDENTIFIER_TOKEN:
                        if (prevTokenType != null && prevTokenType != TokenType.OPERATION_TOKEN) throw new Exception();
                        if (nextTokenType != null && nextTokenType != TokenType.OPERATION_TOKEN) throw new Exception();
                        break;
                    case TokenType.OPERATION_TOKEN:
                        if (nextTokenType == null) throw new Exception();
                        if (nextTokenType == TokenType.OPERATION_TOKEN) throw new Exception();
                        if (prevTokenType == null)
                        {
                            Console.WriteLine(tokens[i].TokenName);
                            throw new Exception();
                        }
                        if (prevTokenType == TokenType.OPERATION_TOKEN) throw new Exception();
                        break;
                    case TokenType.BRACKETS_TOKEN_GROUP:
                        if (prevTokenType != null && prevTokenType != TokenType.OPERATION_TOKEN) throw new Exception();
                        if (nextTokenType != null && nextTokenType != TokenType.OPERATION_TOKEN) throw new Exception();
                        CheckErrors((tokens[i] as TokenBracketsGroup).GetTokens());
                        break;
                }
            }
        }

        private static List<Token> RemoveBrackets(List<Token> tokens)
        {
            List<Token> newTokens = new();
            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                if (token.TokenName == ")")
                {
                    Console.WriteLine(i);
                    throw new Exception();
                }
                if (token.TokenName == "(")
                {
                    int j;
                    var counter = 1;
                    for (j = i + 1; j < tokens.Count; j++)
                    {
                        var lastToken = tokens[j];
                        if (lastToken.TokenName == "(") counter++;
                        if (lastToken.TokenName == ")") counter--;
                        if (counter == 0)
                        {
                            break;
                        }
                    }
                    if (counter != 0)
                    {
                        Console.WriteLine("i = " + i);
                        Console.WriteLine("j = " + j);
                        throw new Exception();
                    }
                    if (j - i == 1) throw new Exception();
                    List<Token> sublist = tokens.GetRange(i + 1, j - i - 1);
                    newTokens.Add(new TokenBracketsGroup(RemoveBrackets(sublist)));
                    i = j;
                }
                else
                {
                    newTokens.Add(token);
                }
            }
            return newTokens;
        }
    }
}
