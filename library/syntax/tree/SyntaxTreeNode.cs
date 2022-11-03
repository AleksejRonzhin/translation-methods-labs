using library.tokens;

namespace library.syntax.tree
{
    public class SyntaxTreeNode
    {
        public TokenInfo Value { get; }
        public List<SyntaxTreeNode> Children { get; } = new();

        public SyntaxTreeNode(TokenInfo value)
        {
            Value = value;
        }

        public void AddChild(SyntaxTreeNode child)
        {
            Children.Add(child);
        }

        public void ReplaceChild(SyntaxTreeNode child, SyntaxTreeNode newChild)
        {

            var index = Children.IndexOf(child);
            Children[index] = newChild;
        }
    }
}