using library.compiler.core.tokens;
using library.compiler.syntax.exceptions;
using library.compiler.syntax.tree;
using System;

namespace library.compiler.syntax
{
    public class SyntaxAnalyzer
    {
        public static SyntaxTree Analyze(List<TokenInfo> tokenInfos)
        {
            try
            {
                var tokensWithoutBrackets = BracketsTakeOuter.TakeOutBrackets(tokenInfos);
                SyntaxChecker.CheckSyntax(tokensWithoutBrackets);
                return SyntaxTreeCreator.Create(tokensWithoutBrackets);
            }
            #region Обработка ошибок
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
            }
            catch (OperandNotFoundException ex)
            {
                throw new SyntaxAnalyzerException(ex.Position, $"У операции {ex.OperationToken} не найден операнд.");
            }
            catch (OperationBetweenOperandsNotFoundException ex)
            {
                throw new SyntaxAnalyzerException(ex.Position, $"Между операндами {ex.FirstOperand} и {ex.SecondOperand} отсуствует операция.");
            }
            #endregion
        }
    }
}
