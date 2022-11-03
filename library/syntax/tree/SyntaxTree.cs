using library.tokens;
using library.tree;

namespace library.syntax.tree
{
    public class SyntaxTree
    {
        public static readonly SyntaxTree Empty = 
            new(new TreeNode<Token>(Token.Empty));

        public TreeNode<Token> HeadNode { get; }

        public SyntaxTree(TreeNode<Token> headNode)
        {
            HeadNode = headNode;
        }
    }
}
