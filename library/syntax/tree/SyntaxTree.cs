using library.tokens;

namespace library.syntax.tree
{
    public class SyntaxTree
    {
        public static readonly SyntaxTree Empty = 
            new(new SyntaxTreeNode(Token.Empty));

        public SyntaxTreeNode HeadNode { get; }

        public SyntaxTree(SyntaxTreeNode headNode)
        {
            HeadNode = headNode;
        }
    }
}
