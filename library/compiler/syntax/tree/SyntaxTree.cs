using library.compiler.core.tokens;

namespace library.compiler.syntax.tree
{
    public class SyntaxTree
    {
        public static readonly SyntaxTree Empty =
            new(new SyntaxTreeNode(TokenInfo.Empty));

        public SyntaxTreeNode HeadNode { get; }

        public SyntaxTree(SyntaxTreeNode headNode)
        {
            HeadNode = headNode;
        }
    }
}
