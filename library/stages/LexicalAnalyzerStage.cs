using library.lexis;
using library.tokens;

namespace library.stages
{
    public class LexicalAnalyzerStage : Stage<List<TokenInfo>>
    {
        private readonly TextReader reader;
        private readonly SymbolsTable symbolsTable;

        public LexicalAnalyzerStage(TextReader textReader, SymbolsTable symbolsTable, Action<List<TokenInfo>> postAction)
            : base(postAction)
        {
            reader = textReader;
            this.symbolsTable = symbolsTable;
        }

        public override List<TokenInfo> GetResult()
        {
            Console.WriteLine("Lexical analyzer started");
            var tokens = LexicalAnalyzer.Analyze(reader, symbolsTable);
            Console.WriteLine("Tokens:");
            tokens.ForEach(token => Console.WriteLine(token));
            Console.WriteLine();
            Console.WriteLine("Symbols table:");
            Console.WriteLine(symbolsTable.ToString());
            return tokens;
        }
    }
}
