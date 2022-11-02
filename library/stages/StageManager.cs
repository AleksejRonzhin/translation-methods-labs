using library.symbols;
using library.syntax.tree;
using library.tokens;

namespace library.stages
{
    internal class StageManager
    {
        private static readonly Action<List<TokenInfo>> emptyLexicalAnalyzerAction = tokens => { };
        private static readonly Action<SyntaxTree> emptySyntaxAnalyzerAction = tree => { };
        private static readonly Action<SyntaxTree> emptySemanticAnalyzerAction = tree => { };


        private readonly Lazy<LexicalAnalyzerStage> lexicalStage;
        private readonly Lazy<SyntaxAnalyzerStage> syntaxStage;
        private readonly Lazy<SemanticAnalyzerStage> semanticStage;

        public LexicalAnalyzerStage LexicalAnalyzerStage
        {
            get { return lexicalStage.Value; }
        }

        public SyntaxAnalyzerStage SyntaxAnalyzerStage
        {
            get { return syntaxStage.Value; }
        }

        public SemanticAnalyzerStage SemanticAnalyzerStage
        {
            get { return semanticStage.Value; }
        }

        internal StageManager(TextReader textReader,
            SymbolsTable symbolsTable,
            Action<List<TokenInfo>>? lexicalAnalyzerAction = null,
            Action<SyntaxTree>? syntaxAnalyzerAction = null,
            Action<SyntaxTree>? semanticAnalyzerAction = null
            )
        {
            lexicalStage = new(new LexicalAnalyzerStage(textReader, symbolsTable, lexicalAnalyzerAction ?? emptyLexicalAnalyzerAction));
            syntaxStage = new Lazy<SyntaxAnalyzerStage>(new SyntaxAnalyzerStage(lexicalStage.Value, syntaxAnalyzerAction ?? emptySyntaxAnalyzerAction));
            semanticStage = new Lazy<SemanticAnalyzerStage>(new SemanticAnalyzerStage(syntaxStage.Value, semanticAnalyzerAction ?? emptySemanticAnalyzerAction, symbolsTable));
        }
    }
}