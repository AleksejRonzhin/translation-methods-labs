using library.tokens;
using library.tree;
using System.Text;

namespace library.syntax.tree
{
    public class SyntaxTreeNode : TreeNode<Token>
    {
        public SyntaxTreeNode(Token value) : base(value)
        {
        }
    }
}
