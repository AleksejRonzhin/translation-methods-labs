using library.lexis.creators;
using library.lexis.exceptions;
using library.tokens;

namespace library.lexis
{
    public class LexicalAnalyzer
    {
        public static (List<TokenInfo> tokens,  SymbolsTable table) Analyze(TextReader textReader)
        {
            var reader = new PositionTextReader(textReader);
            List<TokenInfo> tokens = new();
            SymbolsTable symbolsTable = new();
            List<TokenCreator> creatorList = new()
            {
                new IdentifierTokenCreator(symbolsTable),
                new OperationTokenCreator(),
                new CurlyBracesTokenCreator(),
                new ConstantTokenCreator()
            };

            (int position, char symbol, bool isLast) = reader.Read();
            TokenInfo? lastToken = null;
            TokenInfo? token = null;
            try
            {
                while (!isLast)
                {
                    bool wasToken = false;
                    foreach (TokenCreator creator in creatorList)
                    {
                        if (creator.Start(symbol, position + 1))
                        {
                            do
                            {
                                (position, symbol, isLast) = reader.Read();
                            } while (!isLast && creator.AddSymbol(symbol));

                            token = creator.GetToken();
                            CheckTokensConflict(token, lastToken);
                            tokens.Add(token);

                            lastToken = token;
                            wasToken = true;
                            break;
                        }
                    }
                    if (wasToken) continue;

                    if (symbol != ' ') throw new InvalidSymbolException(symbol);
                    lastToken = null;
                    (position, symbol, isLast) = reader.Read();
                }
                return (tokens, symbolsTable);
            }
            #region Обработка ошибок
            catch (TokensConflictException ex)
            {
                int conflictPosition = (token == null) ? position : token.Position;
                throw new LexicalAnalyzerException(conflictPosition, $"Между лексемами {ex.FirstToken.Token.TokenName} и {ex.SecondToken.Token.TokenName} отсутствует пробел");
            }
            catch (InvalidSymbolException ex)
            {
                throw new LexicalAnalyzerException(position, $"Встречен недопустимый символ {ex.Symbol}");
            }
            catch (InvalidConstantException ex)
            {
                throw new LexicalAnalyzerException(position, $"Неправильно задана константа {ex.Constant}");
            }
            #endregion
        }

        private static void CheckTokensConflict(TokenInfo currentTokenInfo, TokenInfo? prevTokenInfo)
        {
            if (prevTokenInfo == null) return;
            if (currentTokenInfo.Token is IdentifierToken && prevTokenInfo.Token is ConstantToken)
                throw new TokensConflictException(prevTokenInfo, currentTokenInfo);
        }
    }
}