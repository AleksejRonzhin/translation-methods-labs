using library.compiler.core.symbols;
using library.compiler.core.tokens;
using library.compiler.syntax.tree;

namespace library.compiler.stages
{
    public class StageCreator
    {
        private readonly TextReader textReader;
        private readonly SymbolsTable symbolsTable;

        public StageCreator(TextReader textReader, SymbolsTable symbolsTable)
        {
            this.textReader = textReader;
            this.symbolsTable = symbolsTable;
        }

        public IStage CreateLexicalAnalyzerStage(Action<List<TokenInfo>> lexicalAnalyzerAction)
        {
            StageManager stageManager = new(textReader, symbolsTable, lexicalAnalyzerAction);
            return stageManager.LexicalAnalyzerStage;
        }

        public IStage CreateSyntaxAnalyzerStage(Action<SyntaxTree> syntaxAnalyzerAction)
        {
            StageManager stageManager = new(textReader, symbolsTable, null, syntaxAnalyzerAction);
            return stageManager.SyntaxAnalyzerStage;
        }

        public IStage CreateSemanticAnalyzerStage(Action<SyntaxTree> semanticAnalyzerAction)
        {
            StageManager stageManager = new(textReader, symbolsTable, null, null, semanticAnalyzerAction);
            return stageManager.SemanticAnalyzerStage;
        }
    }
}