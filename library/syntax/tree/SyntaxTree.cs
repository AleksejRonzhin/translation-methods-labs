namespace library.syntax.tree
{
    public class SyntaxTree
    {
        public SyntaxTreeNode HeadNode { get; }

        public SyntaxTree(SyntaxTreeNode headNode)
        {
            HeadNode = headNode;
        }
    }
}
