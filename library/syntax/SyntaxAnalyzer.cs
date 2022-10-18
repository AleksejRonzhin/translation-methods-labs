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
                CheckSyntax(tokensWithoutBrackets);
                return CreateSyntaxTree(tokensWithoutBrackets);
            }
            catch (OpenBracketNotFountException ex)
            {
                throw new SyntaxAnalyzerException(ex.Position, "У закрывающей скобки <)> отсуствует открывающая скобка <(>.");
            }
            catch (CloseBracketNotFoundException ex)
            {
                throw new SyntaxAnalyzerException(ex.Position, "У открывающей скобки <(> отсуствует закрывающая скобка <)>.");
            }
            catch (SpaceBetweenBracketsException ex)
            {
                throw new SyntaxAnalyzerException(ex.OpenBracketPosition, "Между скобками ничего нет.");
            } catch (OperandNotFoundException ex)
            {
                throw new SyntaxAnalyzerException(ex.Position, $"У операции {ex.OperationToken} не найден операнд.");
            } catch (OperationBetweenOperandsNotFoundException ex)
            {
                throw new SyntaxAnalyzerException(ex.Position, $"Между операндами {ex.FirstOperand} и {ex.SecondOperand} отсуствует операция.");
            }
        }

        private static SyntaxTree CreateSyntaxTree(List<TokenInfo> tokens)
        {
            return null;
        }

        private static void CheckSyntax(List<TokenInfo> tokenInfos)
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

        private static List<TokenInfo> TakeOutBrackets(List<TokenInfo> tokenInfos)
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
