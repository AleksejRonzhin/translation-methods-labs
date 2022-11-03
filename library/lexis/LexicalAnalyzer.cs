using library.lexis.exceptions;
using library.symbols;
using library.tokens;
using library.tokens.creation;
using library.tokens.creation.exceptions;

namespace library.lexis
{
    public class LexicalAnalyzer
    {
        private static readonly List<char> separatingSymbols = new() { ' ' };

        public static List<TokenInfo> Analyze(TextReader textReader, SymbolsTable symbolsTable)
        {
            var helper = new LexicalAnalyzerHelper(symbolsTable);
            var reader = new PositionTextReader(textReader);
            return WrapLexicalExceptions(() =>
            {
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

                    CheckSymbolCorrectness(position, symbol);
                    helper.RefreshPrevTokenInfo();
                    (position, symbol, isLast) = reader.Read();
                }
                return helper.GetTokens();
            });
        }

        public static List<TokenInfo> Analyze(string text, SymbolsTable symbolsTable)
        {
            var helper = new LexicalAnalyzerHelper(symbolsTable);

            return WrapLexicalExceptions(() =>
            {
                var i = 0;
                while (i < text.Length)
                {
                    var starter = helper.TryGetTokenCreationStarter(text[i]);
                    if (starter != null)
                    {
                        var process = starter.Start(text[i], i);
                        TokenInfo token = PullToken(text, ref i, process);
                        helper.AddToken(token);
                        continue;
                    }

                    CheckSymbolCorrectness(text, i);
                    helper.RefreshPrevTokenInfo();
                    i++;
                }
                return helper.GetTokens();
            });
        }

        private static T WrapLexicalExceptions<T>(Func<T> lambda)
        {
            try
            {
                return lambda.Invoke();
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
            catch (OperationNotFoundException ex)
            {
                throw new LexicalAnalyzerException(ex.Position, $"Операция {ex.Lexeme} не найдена.");
            }
            catch (InvalidIdentifierException ex)
            {
                throw new LexicalAnalyzerException(ex.Position, $"Неправильно задана переменная {ex.Identifier}");
            }
            catch (OperandTypeNotDefinedException ex)
            {
                throw new LexicalAnalyzerException(ex.Position, $"Не определен тип {ex.OperandTypeLine} в лексеме {ex.Lexeme}");
            }
            catch (ConflictIdentifierTypesException ex)
            {
                throw new LexicalAnalyzerException(ex.Position, 
                    $"Конфликт типов у токена {ex.TokenName}. Встречены типы {ex.FirstOperandType} и {ex.SecondOperandType}.");
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

        private static TokenInfo PullToken(string text, ref int i, TokenCreationProcess process)
        {
            do
            {
                i++;
            } while (i < text.Length && process.AddSymbol(text[i]));
            return process.Finish();
        }

        private static void CheckSymbolCorrectness(string text, int i)
        {
            CheckSymbolCorrectness(i, text[i]);
        }

        private static void CheckSymbolCorrectness(int position, char symbol)
        {
            if (!separatingSymbols.Contains(symbol)) throw new InvalidSymbolException(symbol, position);
        }
    }
}