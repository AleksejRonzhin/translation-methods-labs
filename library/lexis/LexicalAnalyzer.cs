using library.tokens;
using library.tokens.creators;
using library.tokens.exceptions;

namespace library.lexis
{
    public class LexicalAnalyzer
    {
        public static (List<Token> tokens,  SymbolsTable table) Analyze(TextReader textReader)
        {
            var reader = new PositionTextReader(textReader);
            List<Token> tokens = new();
            SymbolsTable symbolsTable = new();
            List<TokenCreator> creatorList = new()
            {
                new IdentifierTokenCreator(symbolsTable),
                new OperationTokenCreator(),
                new CurlyBracesTokenCreator(),
                new ConstantTokenCreator()
            };

            (int position, char symbol, bool isLast) = reader.Read();
            Token? lastToken = null;
            Token? token = null;
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
                throw new LexicalAnalyzerException(conflictPosition, $"Между лексемами {ex.FirstToken.TokenName} и {ex.SecondToken.TokenName} отсутствует пробел");
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

        private static void CheckTokensConflict(Token token, Token? lastToken)
        {
            if (lastToken == null) return;
            if (token.Type == TokenType.IDENTIFIER_TOKEN 
                && lastToken.Type == TokenType.CONSTANT_TOKEN) 
                throw new TokensConflictException(lastToken, token);
        }
    }
}