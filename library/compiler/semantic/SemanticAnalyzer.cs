using library.compiler.core.symbols;
using library.compiler.semantic.exceptions;
using library.compiler.syntax.tree;

namespace library.compiler.semantic
{
    public class SemanticAnalyzer
    {
        public static SyntaxTree Analyze(SyntaxTree syntaxTree, SymbolsTable symbolsTable)
        {
            try
            {
                SemanticChecker.CheckSemanticErrors(syntaxTree);
                return new SemanticSyntaxTreeModifier(symbolsTable).Modify(syntaxTree);
            }

            catch (DivisionByZeroException ex)
            {
                throw new SemanticAnalyzerException(ex.TokenInfo.Position, "Деление на ноль.");
            }
        }
    }
}