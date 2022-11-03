using library.symbols;
using library.syntax.tree;
using library.tokens;
using library.tree;

namespace library.semantic
{
    internal class SemanticSyntaxTreeModifier
    {
        private readonly SymbolsTable symbolsTable;

        public SemanticSyntaxTreeModifier(SymbolsTable symbolsTable)
        {
            this.symbolsTable = symbolsTable;
        }

        public SyntaxTree Modify(SyntaxTree syntaxTree)
        {
            var headNode = syntaxTree.HeadNode;
            if (headNode.Value is OperationToken)
            {
                _ = GetOperationResultType(headNode);
            }
            return syntaxTree;
        }

        private OperandType GetOperationResultType(TreeNode<Token> node)
        {
            var operandTypes = GetOperandsTypes(node.Children);
            var requiredType = GetPrioritetType(operandTypes);
            CastChildren(node, requiredType);
            return requiredType;
        }

        private HashSet<OperandType> GetOperandsTypes(List<TreeNode<Token>> children)
        {
            var operandTypes = new HashSet<OperandType>();
            children.ForEach(child => operandTypes.Add(GetOperandTypeNode(child)));
            return operandTypes;
        }

        private OperandType GetOperandTypeNode(TreeNode<Token> node)
        {
            var token = node.Value;
            if (token is IdentifierToken identifierToken)
            {
                var index = identifierToken.AttributeValue;
                if (index == null) throw new Exception();
                return symbolsTable.GetById((int)index).OperandType;

            }
            if (token is ConstantToken constantToken)
            {
                return constantToken.GetOperandType();
            }
            if (token is OperationToken)
            {
                return GetOperationResultType(node);
            }
            if (token is FunctionToken functionToken)
            {
                return functionToken.ResultType;
            }
            return OperandType.NOT_DEFINED;
        }

        private static OperandType GetPrioritetType(HashSet<OperandType> operandTypes)
        {
            if (operandTypes.Count == 1) return operandTypes.First();
            return OperandType.REAL;
        }

        private void CastChildren(TreeNode<Token> node, OperandType requiredType)
        {
            List<(TreeNode<Token> oldNode, TreeNode<Token> newNode)> replacePairs = new();
            node.Children.ForEach(child =>
            {
                if (GetOperandTypeNode(child) != requiredType)
                {
                    var castNode = new TreeNode<Token>(new CastIntToRealFunctionToken());
                    castNode.AddChild(child);
                    replacePairs.Add((child, castNode));
                }
            });
            replacePairs.ForEach(pair => node.ReplaceChild(pair.oldNode, pair.newNode));
        }
    }
}