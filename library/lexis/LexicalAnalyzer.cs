using library.lexis.exceptions;
using library.tokens;
using library.tokens.creation;

namespace library.lexis
{
    public class LexicalAnalyzer
    {
        private static readonly List<char> separatingSymbols = new() { ' ' };

        public static (List<TokenInfo> tokens, SymbolsTable table) Analyze(TextReader textReader)
        {
            var helper = new LexicalAnalyzerHelper();

            try
            {
                var reader = new PositionTextReader(textReader);
                (int position, char symbol, bool isLast) = reader.Read();
                while (!isLast)
                {
                    var starter = helper.TryGetTokenCreationStarter(symbol);
                    if (starter != null)
                    {
                        var process = starter.Start(symbol, position);
                        TokenInfo token = GetToken(reader, ref position, ref symbol, out isLast, process);
                        helper.AddToken(token);
                        continue;
                    }

                    TryMoveToNextSymbol(helper, reader, ref position, ref symbol, out isLast);
                }
                return helper.GetInfo();
            }
            #region Обработка ошибок
            catch (TokensConflictException ex)
            {
                throw new LexicalAnalyzerException(ex.Position, $"Между лексемами {ex.FirstToken.Token.TokenName} и {ex.SecondToken.Token.TokenName} отсутствует пробел");
            }
            catch (InvalidSymbolException ex)
            {
                throw new LexicalAnalyzerException(ex.Position, $"Встречен недопустимый символ {ex.Symbol}");
            }
            catch (InvalidConstantException ex)
            {
                throw new LexicalAnalyzerException(ex.Position, $"Неправильно задана константа {ex.Constant}");
            }
            #endregion
        }

        private static TokenInfo GetToken(PositionTextReader reader, ref int position,
            ref char symbol, out bool isLast, TokenCreationProcess process)
        {
            do
            {
                (position, symbol, isLast) = reader.Read();
            } while (process.AddSymbol(symbol));
            return process.Finish();
        }

        private static void TryMoveToNextSymbol(LexicalAnalyzerHelper helper, PositionTextReader reader, 
            ref int position, ref char symbol, out bool isLast)
        {
            if (separatingSymbols.Contains(symbol))
            {
                helper.RefreshPrevTokenInfo();
                (position, symbol, isLast) = reader.Read();
            }
            else throw new InvalidSymbolException(symbol, position);
        }
    }
}