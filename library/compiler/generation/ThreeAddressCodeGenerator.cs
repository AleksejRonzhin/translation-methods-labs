using library.compiler.core.models;
using library.compiler.optimization;
using library.compiler.syntax.tree;

namespace library.compiler.generation
{
    public class ThreeAddressCodeGenerator
    {
        public static ThreeAddressCode Generate(SyntaxTree modifierSyntaxTree, SymbolsTable symbolsTable, bool withOptimization)
        {
            SyntaxTree syntaxTree = modifierSyntaxTree;
            if (withOptimization)
            {
                syntaxTree = SyntaxTreeOptimizer.Optimize(syntaxTree);
            }

            ThreeAddressCodeGeneratorHelper helper = new(symbolsTable);
            helper.Start(syntaxTree);

            var result = helper.Result;
            if (withOptimization)
                result = ThreeAddressCodeOptimizer.Optimize(result, symbolsTable);
            return result;
        }
    }
}
