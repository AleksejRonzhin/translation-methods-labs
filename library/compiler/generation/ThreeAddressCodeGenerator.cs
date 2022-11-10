using library.compiler.core.models;

namespace library.compiler.generation
{
    public class ThreeAddressCodeGenerator
    {
        public static ThreeAddressCode Generate(SyntaxTree modifierSyntaxTree, SymbolsTable symbolsTable)
        {
            ThreeAddressCodeGeneratorHelper helper = new(symbolsTable);
            helper.Start(modifierSyntaxTree);
            return helper.Result;
        }
    }
}
