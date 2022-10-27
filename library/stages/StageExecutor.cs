using library.lexis;
using library.syntax;
using library.tokens;

namespace library.stages
{
    public class StageExecutor
    {
        public static object Execute(Stage stage, TextReader inputTextReader)
        {
            (List<TokenInfo> tokens, SymbolsTable table) = LexicalAnalyzer.Analyze(inputTextReader);

            if (stage == Stage.LEX) return (tokens, table);
            
            var syntaxTree = SyntaxAnalyzer.Analyze(tokens);
            
            if (stage == Stage.SYN) return syntaxTree;
            
            throw new Exception();
        }
    }
}
