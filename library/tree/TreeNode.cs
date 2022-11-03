using System.Xml.Linq;

namespace library.tree
{
    public class TreeNode<T>
    {
        public T Value { get; }
        public List<TreeNode<T>> Children { get; } = new();

        public TreeNode(T value)
        {
            Value = value;
        }

        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }

        public void ReplaceChild(TreeNode<T> child, TreeNode<T> newChild)
        {

            var index = Children.IndexOf(child);
            Children[index] = newChild;
        }
    }
}