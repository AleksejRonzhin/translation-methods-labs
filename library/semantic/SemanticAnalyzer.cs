using library.operations;
using library.symbols;
using library.syntax.tree;
using library.tokens;
using library.tree;

namespace library.semantic
{
    public class SemanticAnalyzer
    {
        public static SyntaxTree Analyze(SyntaxTree syntaxTree, SymbolsTable symbolsTable)
        {
            ModifySyntaxTree(syntaxTree, symbolsTable);
            return syntaxTree;
        }

        private static void ModifySyntaxTree(SyntaxTree syntaxTree, SymbolsTable symbolsTable)
        {
            var headNode = syntaxTree.HeadNode;
            if (headNode.Value is OperationToken operationToken)
            {
                Console.WriteLine("Cast");
                CastOperandTypes(headNode, symbolsTable, operationToken.Operation);
            }
        }

        private static OperandType CastOperandTypes(TreeNode<Token> node, SymbolsTable symbolsTable, Operation operation)
        {
            HashSet<OperandType> operandTypes = new HashSet<OperandType>();
            node.Childrens.ForEach(children =>
            {
                OperandType type = GetOperandTypeNode(children, symbolsTable);
                operandTypes.Add(type);
            });

            //if (operandTypes.Count == 1) return operandTypes.First();

            var prioritetType = GetPrioritetType(operandTypes);
            for (int i = 0; i < node.Childrens.Count; i++)
            {
                var children = node.Childrens[i];
                if (GetOperandTypeNode(children, symbolsTable) != prioritetType)
                {
                    var castNode = new TreeNode<Token>(Token.Empty);
                    castNode.AddChild(children);
                    var index = node.Childrens.IndexOf(children);
                    node.Childrens[index] = castNode;
                }
            }
            return prioritetType;
        }

        private static OperandType GetOperandTypeNode(TreeNode<Token> node, SymbolsTable symbolsTable)
        {
            var token = node.Value;
            if (token is IdentifierToken)
            {
                return OperandType.INTEGER;
            }
            if (token is ConstantToken)
            {
                return OperandType.INTEGER;
            }
            if (token is OperationToken operationToken)
            {
                return CastOperandTypes(node, symbolsTable, operationToken.Operation);
            }
            if(token == Token.Empty)
            {
                return OperandType.REAL;
            }
            Console.WriteLine(token);
            throw new Exception();
        }

        private static OperandType GetPrioritetType(HashSet<OperandType> operandTypes)
        {
            return OperandType.REAL;
        }
    }
}
