using library.compiler.core.models;
using library.compiler.core.tokens;
using library.compiler.syntax.tree;
using System.Text;

namespace library.compiler.generation
{
    internal class InfixNotationGenerator
    {
        public static string Generate(SyntaxTree syntaxTree)
        {
            return Rec(syntaxTree.HeadNode);
        }

        private static string Rec(SyntaxTreeNode node)
        {
            StringBuilder stringBuilder = new();

            if(node.Children.Count == 0)
            {
                stringBuilder.Append(node.Value.Token.TokenName);
            }
            if (node.Children.Count == 1)
            {
                var firstOperand = node.Children[0];
                stringBuilder.Append(node.Value.Token.TokenName);
                stringBuilder.Append($"({Rec(firstOperand)})");
            }
            if (node.Children.Count == 2)
            {
                var firstOperand = node.Children[0];
                var secondOperand = node.Children[1];

                if(node.Value.Token is OperationToken operationToken && (operationToken.Operation.Sign == "*" || operationToken.Operation.Sign == "/"))
                {
                    stringBuilder.Append(Rec(firstOperand));
                    stringBuilder.Append($" {node.Value.Token.TokenName} ");
                    stringBuilder.Append(Rec(secondOperand));
                } else
                {
                    stringBuilder.Append($"({Rec(firstOperand)}");
                    stringBuilder.Append($" {node.Value.Token.TokenName} ");
                    stringBuilder.Append($"{Rec(secondOperand)})");
                }
            }

            return stringBuilder.ToString();
        }

    }
}
