using library.tokens;
using System.Text;

namespace library.syntax.tree
{
    public class SyntaxTreePrinter
    {
        public static string Print(SyntaxTree syntaxTree)
        {
            if (syntaxTree == SyntaxTree.Empty) return String.Empty;
            return PrintNode(syntaxTree.HeadNode);
        }

        private static string PrintNode(SyntaxTreeNode node, int pledge = 0)
        {
            StringBuilder stringBuilder = new(node.Value.ToString());
            node.Children.ForEach(child =>
            {
                stringBuilder.Append(GetPrefix(pledge))
                .Append(PrintNode(child, pledge + 1));
            });
            return stringBuilder.ToString();
        }

        private static string GetPrefix(int pledge)
        {
            var indent = "";
            for (int i = 0; i < pledge; i++)
            {
                indent += "   ";
            }
            return $"\n{indent}|---";
        }
    }
}
