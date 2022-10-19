namespace library.tree
{
    public class TreeNode<T>
    {
        public T Value { get; }
        public List<TreeNode<T>> Childrens { get; } = new();

        public TreeNode(T value)
        {
            Value = value;
        }

        public void AddChild(TreeNode<T> child)
        {
            Childrens.Add(child);
        }
    }
}