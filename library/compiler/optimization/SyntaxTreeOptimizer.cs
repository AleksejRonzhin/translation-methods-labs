using library.compiler.core.models;
using library.compiler.core.tokens;
using library.compiler.generation;
using library.compiler.syntax.tree;

namespace library.compiler.optimization
{
    internal class SyntaxTreeOptimizer
    {
        public static SyntaxTree Optimize(SyntaxTree syntaxTree)
        {

            SyntaxTree optimizedSyntaxTree = OptimizeTree(syntaxTree);
            Print(syntaxTree, optimizedSyntaxTree);
            return optimizedSyntaxTree;
        }

        private static SyntaxTree OptimizeTree(SyntaxTree syntaxTree)
        {
            return new(TryOptimizeNode(syntaxTree.HeadNode));
        }

        private static void Print(SyntaxTree syntaxTree, SyntaxTree newSyntaxTree)
        {
            Console.WriteLine("Before optimization:");
            Console.WriteLine(InfixNotationGenerator.Generate(syntaxTree));
            Console.WriteLine("After optimization:");
            Console.WriteLine(InfixNotationGenerator.Generate(newSyntaxTree));
        }

        private static SyntaxTreeNode TryOptimizeNode(SyntaxTreeNode node)
        {
            var optimizedNode = OptimizeChildren(node);
            optimizedNode = TryOptimizeOperation(optimizedNode);
            optimizedNode = TryOptimizeCast(optimizedNode);
            return optimizedNode;
        }

        private static SyntaxTreeNode OptimizeChildren(SyntaxTreeNode node)
        {
            var copyNode = new SyntaxTreeNode(node.Value);
            node.Children.ForEach(child =>
            {
                var optimizedChild = TryOptimizeNode(child);
                copyNode.AddChild(optimizedChild);
            });
            return copyNode;
        }

        private static SyntaxTreeNode TryOptimizeOperation(SyntaxTreeNode node)
        {
            if (node.Value.Token is OperationToken operationToken)
            {
                var firstOperand = node.Children[0].Value.Token;
                var secondOperand = node.Children[1].Value.Token;
                bool firstIsConstant = firstOperand is ConstantToken;
                bool secondIsConstant = secondOperand is ConstantToken;

                if (firstIsConstant && secondIsConstant) return CalculateOperation(operationToken.Operation, (ConstantToken)firstOperand, (ConstantToken)secondOperand);
                if (firstIsConstant) return TryOptimizeFirstOperand(operationToken.Operation, (ConstantToken)firstOperand, node);
                if (secondIsConstant) return TryOptimizeSecondOperand(operationToken.Operation, (ConstantToken)secondOperand, node);
            }
            return node;
        }

        private static SyntaxTreeNode CalculateOperation(Operation operation, ConstantToken firstToken, ConstantToken secondToken)
        {
            double firstValue = double.Parse(firstToken.TokenName, ConstantToken.numberFormatInfo);
            double secondValue = double.Parse(secondToken.TokenName, ConstantToken.numberFormatInfo);
            double result = operation.Sign switch
            {
                "+" => firstValue + secondValue,
                "-" => firstValue - secondValue,
                "*" => firstValue * secondValue,
                "/" => firstValue / secondValue,
                _ => 0
            };
            OperandType resultType = firstToken.GetOperandType();
            return CreateSyntaxTreeConstantNode(resultType, operation, result);
        }

        private static SyntaxTreeNode CreateSyntaxTreeConstantNode(OperandType resultType, Operation operation, double value)
        {
            string text;
            string tokenName;
            if (resultType == OperandType.REAL || operation.Sign == "/")
            {
                tokenName = value.ToString(ConstantToken.numberFormatInfo);
                if (!tokenName.Contains('.')) tokenName += ".0";
                text = "константа вещественного числа";
            }
            else
            {
                tokenName = ((int)value).ToString();
                text = "константа целого числа";
            }

            return new SyntaxTreeNode(new TokenInfo(new ConstantToken(tokenName), -1, text));
        }

        private static SyntaxTreeNode TryOptimizeSecondOperand(Operation operation, ConstantToken secondOperand, SyntaxTreeNode sourceNode)
        {
            var operandType = secondOperand.GetOperandType();
            var tokenName = secondOperand.TokenName;
            double value = double.Parse(tokenName, ConstantToken.numberFormatInfo);

            return operation.Sign switch
            {
                "+" or "-" when value == 0 => LeaveOneOperand(sourceNode, 0),
                "*" when value == 0 => GetZeroNode(operandType),
                "*" or "/" when value == 1 => LeaveOneOperand(sourceNode, 0),
                _ => sourceNode
            };
        }

        private static SyntaxTreeNode TryOptimizeFirstOperand(Operation operation, ConstantToken firstOperand, SyntaxTreeNode sourceNode)
        {
            var operandType = firstOperand.GetOperandType();
            var tokenName = firstOperand.TokenName;
            double value = double.Parse(tokenName, ConstantToken.numberFormatInfo);

            return operation.Sign switch
            {
                "+" when value == 0 => LeaveOneOperand(sourceNode, 1),
                "*" or "/" when value == 0 => GetZeroNode(operandType),
                "*" when value == 1 => LeaveOneOperand(sourceNode, 1),
                _ => sourceNode
            };
        }

        private static SyntaxTreeNode LeaveOneOperand(SyntaxTreeNode node, int operandNumber)
        {
            return node.Children[operandNumber];
        }

        private static SyntaxTreeNode GetZeroNode(OperandType operandType)
        {
            (var tokenName, var text) = operandType == OperandType.INTEGER ? ("0", "константа целого числа") : ("0.0", "константа вещественного числа");
            return new SyntaxTreeNode(new TokenInfo(new ConstantToken(tokenName), -1, text));
        }

        private static SyntaxTreeNode TryOptimizeCast(SyntaxTreeNode node)
        {
            if (node.Value.Token is CastIntToRealFunctionToken castToken)
            {
                var operand = node.Children[0].Value.Token;
                if (operand is ConstantToken)
                {
                    return new SyntaxTreeNode(new TokenInfo(new ConstantToken(operand.TokenName + ".0"), -1, "константа вещественного числа"));
                }
            }
            return node;
        }
    }
}
