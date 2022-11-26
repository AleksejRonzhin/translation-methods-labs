using library.compiler.core.models;
using library.compiler.core.tokens;
using library.compiler.syntax.tree;

namespace library.compiler.optimization
{
    internal class SyntaxTreeOptimizer
    {
        public static SyntaxTree Optimize(SyntaxTree syntaxTree)
        {
            Rec(syntaxTree.HeadNode);
            return syntaxTree;
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

            if (node.Value.Token is OperationToken operationToken)
            {
                var firstOperand = node.Children[0].Value.Token;
                var secondOperand = node.Children[1].Value.Token;

                bool firstIsConstant = firstOperand is ConstantToken;
                bool secondIsConstant = secondOperand is ConstantToken;

                if (firstIsConstant && secondIsConstant)
                {
                    Console.WriteLine("2 constants " + operationToken + " " + firstOperand + " " + secondOperand);
                }

                if (firstIsConstant)
                {
                    if (firstOperand.TokenName == "0" || firstOperand.TokenName == "1")
                    {

                        Console.WriteLine("first constant " + firstOperand);
                    }
                }


                if (secondIsConstant)
                {
                    if (secondOperand.TokenName == "0" || secondOperand.TokenName == "1")
                    {

                        Console.WriteLine("second constant " + secondOperand);
                    }
                }
            }

            if(node.Value.Token is CastIntToRealFunctionToken castToken)
            {
                var operand = node.Children[0].Value.Token;
                return new SyntaxTreeNode(new TokenInfo(new ConstantToken(operand.TokenName + ".0"), -1, ""));
            }
            return node;
        }
    }
}
