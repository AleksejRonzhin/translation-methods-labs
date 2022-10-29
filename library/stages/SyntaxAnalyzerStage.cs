using library.syntax;
using library.syntax.tree;

namespace library.stages
{
    public class SyntaxAnalyzerStage : Stage<SyntaxTree>
    {
        private readonly LexicalAnalyzerStage lexicalStage;

        public SyntaxAnalyzerStage(LexicalAnalyzerStage lexicalStage, Action<SyntaxTree> postAction) : base(postAction)
        {
            this.lexicalStage = lexicalStage;
        }

        public override SyntaxTree GetResult()
        {
            var tokens = lexicalStage.GetResult();
            Console.WriteLine("Syntax analyzer started.");
            var tree = SyntaxAnalyzer.Analyze(tokens);
            Console.WriteLine("Syntax tree:");
            Console.WriteLine(SyntaxTreePrinter.Print(tree));
            return tree;
        }
    }
}