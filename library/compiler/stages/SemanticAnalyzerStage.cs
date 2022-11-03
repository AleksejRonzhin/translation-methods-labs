﻿using library.compiler.core.symbols;
using library.compiler.semantic;
using library.compiler.syntax.tree;
using library.tokens;

namespace library.compiler.stages
{
    public class SemanticAnalyzerStage : Stage<SyntaxTree>
    {
        private readonly SyntaxAnalyzerStage syntaxStage;
        private readonly SymbolsTable symbolsTable;

        public SemanticAnalyzerStage(SyntaxAnalyzerStage syntaxStage, Action<SyntaxTree> postAction, SymbolsTable symbolsTable)
            : base(postAction)
        {
            this.syntaxStage = syntaxStage;
            this.symbolsTable = symbolsTable;
        }

        public override SyntaxTree GetResult()
        {
            var syntaxTree = syntaxStage.GetResult();
            Console.WriteLine("Semantic analyzer started.");
            var tree = SemanticAnalyzer.Analyze(syntaxTree, symbolsTable);
            Console.WriteLine("Modificated syntax tree:");
            Console.WriteLine(SyntaxTreePrinter.Print(tree));
            return tree;
        }
    }
}