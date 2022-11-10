using library.compiler.core.models;
using library.compiler.syntax.tree;

namespace library.compiler.generation
{
    internal class PostfixNotationGenerator
    {
        internal static PostfixNotation Generate(SyntaxTree modifierSyntaxTree, SymbolsTable symbolsTable)
        {
            return Rec(modifierSyntaxTree.HeadNode);
        }

        private static PostfixNotation Rec(SyntaxTreeNode node)
        {
            List<PostfixNotation> parts = new();
            node.Children.ForEach(child => parts.Add(Rec(child)));
            parts.Add(new PostfixNotation(node.Value.Token));
            return new PostfixNotation(parts);
        }
    }
}
