using library.compiler.core.tokens;
using library.compiler.syntax.tree;

namespace library.compiler.core.models
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
