using library.compiler.core.models;
using library.compiler.core.tokens;
using library.compiler.syntax.tree;
using System.Globalization;

namespace library.compiler.optimization
{
    internal class SyntaxTreeOptimizer
    {
        public static SyntaxTree Optimize(SyntaxTree syntaxTree)
        {
            Console.WriteLine(SyntaxTreePrinter.Print(syntaxTree));
            Rec(syntaxTree.HeadNode);

            var result = new SyntaxTree(TryOptimize(syntaxTree.HeadNode));
            Console.WriteLine();
            Console.WriteLine(SyntaxTreePrinter.Print(result));
            return result;
        }

        public static void Rec(SyntaxTreeNode node)
        {
            for (int i = 0; i < node.Children.Count; i++)
            {
                var child = node.Children[i];
                Rec(child);
                node.ReplaceChild(child, TryOptimize(child));
            }
        }

        public static SyntaxTreeNode TryOptimize(SyntaxTreeNode node)
        {
            node = TryOptimizeOperation(node);
            node = TryOptimizeCast(node);
            return node;
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
            (var resultTokenName, var text) = resultType == OperandType.REAL || operation.Sign == "/" ? 
                (result.ToString(ConstantToken.numberFormatInfo), "константа вещественного числа") 
                : (((int)result).ToString(), "константа целого числа");
            return new SyntaxTreeNode(new TokenInfo(new ConstantToken(resultTokenName), -1, text));
        }

        private static SyntaxTreeNode TryOptimizeSecondOperand(Operation operation, ConstantToken secondOperand, SyntaxTreeNode sourceNode)
        {
            var operandType = secondOperand.GetOperandType();
            var tokenName = secondOperand.TokenName;
            double value = double.Parse(tokenName, ConstantToken.numberFormatInfo);

            return operation.Sign switch
            {
                "+" or "-" when value == 0 => LeaveOneOperand(sourceNode, 0, operation),
                "*" when value == 0 => GetZeroNode(operandType),
                "*" or "/" when value == 1 => LeaveOneOperand(sourceNode, 0, operation),
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
                "+" when value == 0 => LeaveOneOperand(sourceNode, 1, operation),
                "*" or "/" when value == 0 => GetZeroNode(operandType),
                "*" when value == 1 => LeaveOneOperand(sourceNode, 1, operation),
                _ => sourceNode
            };
        }

        private static SyntaxTreeNode LeaveOneOperand(SyntaxTreeNode node, int operandNumber, Operation operation)
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
                if(operand is ConstantToken)
                {
                    return new SyntaxTreeNode(new TokenInfo(new ConstantToken(operand.TokenName + ".0"), -1, "константа вещественного числа"));
                }
            }
            return node;
        }
    }
}
