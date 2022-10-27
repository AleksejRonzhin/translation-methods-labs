using library.syntax.exceptions;
using library.tokens;

namespace library.syntax
{
    internal class BracketsTakeOuter
    {
        public static List<TokenInfo> TakeOutBrackets(List<TokenInfo> tokenInfos)
        {
            List<TokenInfo> tokensInfoWithoutBrackets = new();
            for (int i = 0; i < tokenInfos.Count; i++)
            {
                var tokenInfo = tokenInfos[i];
                var token = tokenInfo.Token;
                if (token is not BracketToken)
                    tokensInfoWithoutBrackets.Add(tokenInfo);
                else
                {
                    if (token.TokenName == ")") throw new OpenBracketNotFountException(tokenInfo.Position);
                    if (token.TokenName == "(")
                    {
                        (var underBracketsExpression, i) = GetUnderBracketsExpression(tokenInfos, i);
                        var tokensWithoutBrackets = TakeOutBrackets(underBracketsExpression.GetTokens());
                        tokensInfoWithoutBrackets.Add(new UnderBracketsExpression(tokensWithoutBrackets));
                    }
                }
            }
            return tokensInfoWithoutBrackets;
        }

        private static (UnderBracketsExpression, int) GetUnderBracketsExpression(List<TokenInfo> tokenInfos, int startIndex)
        {
            int closedBracketIndex = FindClosedBracket(tokenInfos, startIndex);
            if (closedBracketIndex - startIndex == 1) throw new SpaceBetweenBracketsException(tokenInfos[startIndex].Position);
            var underBracketsExpressionTokens = tokenInfos.GetRange(startIndex + 1, closedBracketIndex - startIndex - 1);
            return (new(underBracketsExpressionTokens), closedBracketIndex);
        }

        private static int FindClosedBracket(List<TokenInfo> tokenInfos, int startIndex)
        {
            // Счетчик незакрытых <(>
            var bracketCounter = 1;
            int currentIndex;
            for (currentIndex = startIndex + 1; currentIndex < tokenInfos.Count; currentIndex++)
            {
                var token = tokenInfos[currentIndex].Token;
                if (token is not BracketToken) continue;

                if (token.TokenName == "(") bracketCounter++;
                if (token.TokenName == ")") bracketCounter--;

                if (bracketCounter == 0) break;
            }
            if (bracketCounter != 0)
                throw new CloseBracketNotFoundException(tokenInfos[startIndex].Position);
            return currentIndex;
        }

    }
}
