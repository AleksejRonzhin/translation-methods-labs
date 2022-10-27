using library.syntax.exceptions;
using library.tokens;

namespace library.syntax
{
    internal class SyntaxChecker
    {
        public static void CheckSyntax(List<TokenInfo> tokenInfos)
        {
            if (tokenInfos.Count == 0) return;

            CheckFirstToken(tokenInfos.First());
            CheckLastToken(tokenInfos.Last());
            TokenInfo currentTokenInfo = tokenInfos.First();
            for (int i = 1; i < tokenInfos.Count; i++)
            {
                TokenInfo nextTokenInfo = tokenInfos[i];
                Token nextToken = nextTokenInfo.Token;

                if (currentTokenInfo is UnderBracketsExpression expression)
                    CheckUnderBracketsExpression(expression);

                Token currentToken = currentTokenInfo.Token;
                if (currentToken is OperationToken operationToken)
                    CheckOperation(operationToken, nextToken, currentTokenInfo.Position);

                if (currentToken is OperandToken operandToken)
                    CheckOperand(operandToken, nextToken, currentTokenInfo.Position);

                currentTokenInfo = nextTokenInfo;
            }
        }

        private static void CheckLastToken(TokenInfo tokenInfo)
        {
            if (tokenInfo.Token is OperationToken token) throw new OperandNotFoundException(token, tokenInfo.Position);
        }

        private static void CheckFirstToken(TokenInfo tokenInfo)
        {
            if (tokenInfo.Token is OperationToken token) throw new OperandNotFoundException(token, tokenInfo.Position);
        }

        private static void CheckUnderBracketsExpression(UnderBracketsExpression underBracketsExpression)
        {
            CheckSyntax(underBracketsExpression.GetTokens());
        }

        private static void CheckOperand(OperandToken token, Token nextToken, int position)
        {
            if (nextToken is OperandToken nextTokenTemp)
                throw new OperationBetweenOperandsNotFoundException(token, nextTokenTemp, position);

        }

        private static void CheckOperation(OperationToken token, Token nextToken, int position)
        {
            if (nextToken is OperationToken) throw new OperandNotFoundException(token, position);
        }

    }
}
