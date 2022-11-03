using library.semantic.exceptions;
using library.syntax.tree;
using library.tokens;

namespace library.semantic
{
    internal class SemanticChecker
    {
        internal static void CheckSemanticErrors(SyntaxTree syntaxTree)
        {
            CheckDivisionByZero(syntaxTree);
        }

        private static void CheckDivisionByZero(SyntaxTree syntaxTree)
        {
            var headNode = syntaxTree.HeadNode;
            CheckDivisionByZeroNode(headNode);
        }

        private static void CheckDivisionByZeroNode(SyntaxTreeNode node)
        {
            if (node.Value.Token is OperationToken operationToken)
            {
                if (operationToken.Operation.Sign == "/")
                {
                    var divider = node.Children[1];
                    if (divider.Value.Token is ConstantToken)
                    {
                        if (Convert.ToDouble(divider.Value.Token.TokenName, System.Globalization.CultureInfo.InvariantCulture) == 0)
                        {
                            throw new DivisionByZeroException(divider.Value);
                        }
                    }
                }
            }
            node.Children.ForEach(child => CheckDivisionByZeroNode(child));
        }
    }
}
