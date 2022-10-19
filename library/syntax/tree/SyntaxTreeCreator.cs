using library.operations;
using library.tokens;

namespace library.syntax.tree
{
    internal class SyntaxTreeCreator
    {
        public static SyntaxTree Create(List<TokenInfo> tokenInfos)
        {
            if (tokenInfos.Count == 0) return SyntaxTree.Empty;

            var parentTreeNode = FindParentTreeNode(tokenInfos);
            if (parentTreeNode == null)
            {
                return new SyntaxTree(new SyntaxTreeNode(tokenInfos.First().Token));
            }
            return new SyntaxTree(parentTreeNode);
        }

        private static SyntaxTreeNode? FindParentTreeNode(List<TokenInfo> tokenInfos)
        {
            if (tokenInfos.Count == 1)
            {
                return CreateSyntaxTreeNode(tokenInfos.First());
            }
            var operations = tokenInfos.FindAll(tokenInfo => tokenInfo.Token is OperationToken);
            var operationsGroups = GroupOpeations(operations);
            foreach (var group in operationsGroups)
            {
                if (group.operations.Count == 0) continue;
                TokenInfo operation = GetOperation(group.operations, group.isAssociative, group.isDirectProcedure);
                int index = tokenInfos.IndexOf(operation);
                if (index != -1) return PullParentTreeNode(tokenInfos, index);
            }
            return null;
        }

        private static TokenInfo GetOperation(List<TokenInfo> operations, bool isAssociative, bool isDirectProcedure)
        {
            return isAssociative ? operations[operations.Count / 2]
                    : isDirectProcedure ? operations.Last() : operations.First();
        }

        private static SyntaxTreeNode? CreateSyntaxTreeNode(TokenInfo tokenInfo)
        {
            if (tokenInfo is UnderBracketsExpression expression)
            {
                return FindParentTreeNode(expression.GetTokens());
            }
            return new SyntaxTreeNode(tokenInfo.Token);
        }

        private static List<(List<TokenInfo> operations, bool isAssociative, bool isDirectProcedure)> GroupOpeations(List<TokenInfo> operations)
        {
            var operationsGroups = new List<(List<TokenInfo>, bool, bool)>();

            for (int i = Operation.MinPrioritet; i <= Operation.MaxPrioritet; i++)
            {
                var group = operations
                    .FindAll(op => ((OperationToken)op.Token).Operation.Prioritet == i)
                    .ToList();
                for (int j = 0; j < 2; j++)
                {
                    bool isAssociative = j == 0;
                    var groupByAssociative =
                        group.FindAll(op => ((OperationToken)op.Token).Operation.IsAssociative == isAssociative)
                        .ToList();
                    for (int k = 0; k < 2; k++)
                    {
                        bool isDirectProcedure = k == 0;
                        var groupByDirect = groupByAssociative
                            .FindAll(op => ((OperationToken)op.Token).Operation.IsDirectProcedure == isDirectProcedure)
                            .ToList();
                        operationsGroups.Add((groupByDirect, isAssociative, isDirectProcedure));
                    }
                }
            }
            return operationsGroups;
        }

        private static SyntaxTreeNode PullParentTreeNode(List<TokenInfo> tokenInfos, int parentIndex)
        {
            var parentTreeNode = new SyntaxTreeNode(tokenInfos[parentIndex].Token);
            var leftTokens = tokenInfos.GetRange(0, parentIndex);
            var leftNode = FindParentTreeNode(leftTokens);
            if (leftNode != null) parentTreeNode.AddChild(leftNode);
            var rightTokens = tokenInfos.GetRange(parentIndex + 1, tokenInfos.Count - parentIndex - 1);
            var rightNode = FindParentTreeNode(rightTokens);
            if (rightNode != null) parentTreeNode.AddChild(rightNode);
            return parentTreeNode;
        }
    }
}
