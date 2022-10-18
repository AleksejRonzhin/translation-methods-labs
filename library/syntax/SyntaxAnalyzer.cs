using library.syntax.exceptions;
using library.tokens;

namespace library.syntax
{
    public class SyntaxAnalyzer
    {
        public static SyntaxTree Analyze(List<TokenInfo> tokens)
        {
            try
            {

                var tokensWithoutBrackets = TakeOutBrackets(tokens);
                tokensWithoutBrackets.ForEach(token => Console.WriteLine(token));
                //CheckErrors(tokensWithoutBrackets);
                return CreateSyntaxTree(tokensWithoutBrackets);
            } catch(OpenBracketNotFountException ex)
            {
                throw new SyntaxAnalyzerException(ex.Position, "У закрывающей скобки <)> отсуствует открывающая скобка <(>.");
            } catch(CloseBracketNotFoundException ex)
            {
                throw new SyntaxAnalyzerException(ex.Position, "У открывающей скобки <(> отсуствует закрывающая скобка <)>.");
            } catch(SpaceBetweenBracketsException ex)
            {
                throw new SyntaxAnalyzerException(ex.OpenBracketPosition, "Между скобками ничего нет.");
            }
        }

        private static SyntaxTree CreateSyntaxTree(List<TokenInfo> tokens)
        {
            return null;
        }

        /*private static void CheckErrors(List<TokenInfo> tokens)
        {
            Console.WriteLine("Begin with " + tokens.First().Token.TokenName);
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
                        if (prevTokenType != null && prevTokenType != TokenType.OPERATION_TOKEN) throw new Exception(); // Слева нет оператора
                        if (nextTokenType != null && nextTokenType != TokenType.OPERATION_TOKEN) throw new Exception(); // Справа нет оператора
                        break;
                    case TokenType.OPERATION_TOKEN:
                        if (nextTokenType == null) throw new Exception(); // Справа нет опернада
                        if (nextTokenType == TokenType.OPERATION_TOKEN) throw new Exception(); // Справа нет операнда
                        if (prevTokenType == null)
                        {
                            Console.WriteLine(tokens[i].TokenName);
                            throw new Exception(); // Слева нет операнда
                        }
                        if (prevTokenType == TokenType.OPERATION_TOKEN) throw new Exception(); // Слева нет операнда
                        break;
                    case TokenType.BRACKETS_TOKEN_GROUP: 
                        if (prevTokenType != null && prevTokenType != TokenType.OPERATION_TOKEN) throw new Exception(); // Слева нет оператора
                        if (nextTokenType != null && nextTokenType != TokenType.OPERATION_TOKEN) throw new Exception(); // Справа нет оператора
                        CheckErrors((tokens[i] as UnderBracketsExpression).GetTokens());
                        break;
                }
            }
        }*/

        private static List<TokenInfo> TakeOutBrackets(List<TokenInfo> tokenInfos)
        {
            List<TokenInfo> tokensWithoutBrackets = new();
            for (int i = 0; i < tokenInfos.Count; i++)
            {
                var tokenInfo = tokenInfos[i];
                if (tokenInfo.Token.TokenName == ")") throw new OpenBracketNotFountException(tokenInfo.Position);
                if (tokenInfo.Token.TokenName == "(")
                {
                    (var sublist, i) = GetUnderBracketsToken(tokenInfos, i);
                    tokensWithoutBrackets.Add(new UnderBracketsExpression(TakeOutBrackets(sublist)));
                }
                else
                {
                    tokensWithoutBrackets.Add(tokenInfo);
                }
            }
            return tokensWithoutBrackets;
        }

        private static (List<TokenInfo>, int) GetUnderBracketsToken(List<TokenInfo> tokenInfos, int startIndex)
        {
            int currentIndex;
            var counter = 1;
            for (currentIndex = startIndex + 1; currentIndex < tokenInfos.Count; currentIndex++)
            {
                var token = tokenInfos[currentIndex].Token;
                if (token.TokenName == "(") counter++;
                if (token.TokenName == ")") counter--;
                if (counter == 0)
                {
                    break;
                }
            }
            if (counter != 0)
                throw new CloseBracketNotFoundException(tokenInfos[startIndex].Position);

            if (currentIndex - startIndex == 1) throw new SpaceBetweenBracketsException(tokenInfos[startIndex].Position);
            
            return (tokenInfos.GetRange(startIndex + 1, currentIndex - startIndex - 1), currentIndex);
        }
    }
}
