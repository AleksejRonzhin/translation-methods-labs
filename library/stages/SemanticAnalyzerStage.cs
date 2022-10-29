using library.syntax.tree;

namespace library.stages
{
    public class SemanticAnalyzerStage : Stage<SyntaxTree>
    {
        private readonly SyntaxAnalyzerStage syntaxStage;

        public SemanticAnalyzerStage(SyntaxAnalyzerStage syntaxStage, Action<SyntaxTree> postAction) : base(postAction)
        {
            this.syntaxStage = syntaxStage;
        }

        public override SyntaxTree GetResult()
        {
            var syntaxTree = syntaxStage.GetResult();
            Console.WriteLine("Semantic analyzer started.");
            var tree = syntaxTree;
            Console.WriteLine("Opt syntax tree:");
            Console.WriteLine(SyntaxTreePrinter.Print(tree));

            return tree;
        }
    }
}