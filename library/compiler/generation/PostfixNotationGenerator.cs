using library.compiler.core.models;
using library.compiler.syntax.tree;
using System.Text;

namespace library.compiler.generation
{
    internal class PostfixNotationGenerator
    {
        internal static string Generate(SyntaxTree modifierSyntaxTree, SymbolsTable symbolsTable)
        {
            return Rec(modifierSyntaxTree.HeadNode);
        }

        private static string Rec(SyntaxTreeNode node)
        {
            StringBuilder stringBuilder = new();
            node.Children.ForEach(child =>
            {
                stringBuilder.Append(Rec(child)).Append(" ");
            });
            stringBuilder.Append(node.Value.Token.ToString());
            return stringBuilder.ToString();
        }
    }
}
