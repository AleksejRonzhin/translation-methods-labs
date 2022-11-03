﻿using library.compiler.core.operations;
using library.compiler.core.symbols;
using library.compiler.core.tokens;
using library.compiler.syntax.tree;

namespace library.compiler.semantic
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
            if (headNode.Value.Token is OperationToken)
            {
                _ = GetOperationResultType(headNode);
            }
            return syntaxTree;
        }

        private OperandType GetOperationResultType(SyntaxTreeNode node)
        {
            var operandTypes = GetOperandsTypes(node.Children);
            var requiredType = GetPrioritetType(operandTypes);
            CastChildren(node, requiredType);
            return requiredType;
        }

        private HashSet<OperandType> GetOperandsTypes(List<SyntaxTreeNode> children)
        {
            var operandTypes = new HashSet<OperandType>();
            children.ForEach(child => operandTypes.Add(GetOperandTypeNode(child)));
            return operandTypes;
        }

        private OperandType GetOperandTypeNode(SyntaxTreeNode node)
        {
            var token = node.Value.Token;
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

        private void CastChildren(SyntaxTreeNode node, OperandType requiredType)
        {
            List<(SyntaxTreeNode oldNode, SyntaxTreeNode newNode)> replacePairs = new();
            node.Children.ForEach(child =>
            {
                if (GetOperandTypeNode(child) != requiredType)
                {
                    var castNode = new SyntaxTreeNode(new TokenInfo(new CastIntToRealFunctionToken(), 0, ""));
                    castNode.AddChild(child);
                    replacePairs.Add((child, castNode));
                }
            });
            replacePairs.ForEach(pair => node.ReplaceChild(pair.oldNode, pair.newNode));
        }
    }
}