using library.compiler.core.models;
using library.compiler.core.tokens;
using library.compiler.syntax.tree;

namespace library.compiler.generation
{
    internal class ThreeAddressCodeGeneratorHelper
    {
        private readonly SymbolsTable _symbolsTable;

        public ThreeAddressCode Result { get; } = new();

        private int _tempCount = 0;

        public ThreeAddressCodeGeneratorHelper(SymbolsTable symbolsTable)
        {
            _symbolsTable = symbolsTable;
        }

        internal void Start(SyntaxTree modifierSyntaxTree)
        {
            _ = Rec(modifierSyntaxTree.HeadNode);
        }

        private (OperandToken result, OperandType type) Rec(SyntaxTreeNode node)
        {
            if (!IsOperation(node)) return ((OperandToken)node.Value.Token, GetOperandType(node));

            (OperandToken operand1, OperandType operand1Type) = Rec(node.Children[0]);
            OperandToken? operand2 = null;
            if(node.Children.Count > 1) (operand2, _) = Rec(node.Children[1]);

            string code = GetOperationCode(node);
            OperandType operandType = GetOperandType(node, operand1Type);
            OperandToken result = GenerateTemp(operandType);

            ThreeAddressLine line = new(code, result, operand1, operand2);
            Result.AddLine(line);
            return (line.Result, operandType);
        }

        private static OperandType GetOperandType(SyntaxTreeNode node, OperandType operand1Type)
        {
            if (node.Value.Token is OperationToken)
            {
                return operand1Type;
            }
            if (node.Value.Token is UnaryFunctionToken functionToken)
            {
                return functionToken.ResultType;
            }
            return OperandType.NOT_DEFINED;
        }

        private OperandType GetOperandType(SyntaxTreeNode node)
        {
            if (node.Value.Token is IdentifierToken identifierToken)
            {
                var index = identifierToken.AttributeValue;
                if (index == null) throw new Exception();
                return _symbolsTable.GetByIndex((int)index).OperandType;
            }
            if (node.Value.Token is ConstantToken constantToken)
            {
                return constantToken.GetOperandType();
            }
            return OperandType.NOT_DEFINED;
        }

        private static bool IsOperation(SyntaxTreeNode node)
        {
            if (node.Value.Token is OperationToken operationToken)
            {
                return true;
            }
            if (node.Value.Token is UnaryFunctionToken functionToken)
            {
                return true;
            }
            return false;
        }

        private OperandToken GenerateTemp(OperandType operandType)
        {
            string tokenName = $"T{++_tempCount}";
            int index = _symbolsTable.GetIndexOrAddSymbol(tokenName, operandType, true);
            return new IdentifierToken(tokenName, index);
        }

        private static string GetOperationCode(SyntaxTreeNode node)
        {
            if (node.Value.Token is OperationToken operationToken)
            {
                return operationToken.Operation.Code;
            }
            if (node.Value.Token is UnaryFunctionToken functionToken)
            {
                return functionToken.Code;
            }
            throw new Exception();
        }
    }
}
