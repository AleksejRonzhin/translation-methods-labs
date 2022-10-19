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

        public string ToString(int pledge = 0)
        {
            var a = "";
            for(int i = 0; i < pledge; i++)
            {
                a += "   ";
            }
            StringBuilder stringBuilder = new(Value.ToString());
            Childrens.ForEach(child =>
            {
                stringBuilder.Append($"\n{a}|---").Append(((SyntaxTreeNode)child).ToString(pledge + 1));
            });
            return stringBuilder.ToString();
        }
    }
}
