using library.compiler.core.models;
using library.compiler.core.tokens;
using library.compiler.generation;
using library.compiler.lexis;
using library.compiler.semantic;
using library.compiler.syntax;

namespace library.compiler
{
    public class Compiler
    {
        private readonly TextReader _inputTextReader;
        public SymbolsTable SymbolsTable { get; } = new();

        private List<TokenInfo>? _tokens;
        private SyntaxTree? _syntaxTree;
        private SyntaxTree? _modifierSyntaxTree;
        private ThreeAddressCode? _threeAddressCode;
        private PostfixNotation? _postfixNotation;

        public List<TokenInfo> Tokens
        {
            get
            {
                return _tokens ??= LexicalAnalyzer.Analyze(_inputTextReader, SymbolsTable);
            }
        }

        public SyntaxTree SyntaxTree
        {
            get
            {
                return _syntaxTree ??= SyntaxAnalyzer.Analyze(Tokens);
            }
        }

        public SyntaxTree ModifierSyntaxTree
        {
            get
            {
                return _modifierSyntaxTree ??= SemanticAnalyzer.Analyze(SyntaxTree, SymbolsTable);
            }
        }

        public ThreeAddressCode ThreeAddressCode
        {
            get
            {
                return _threeAddressCode ??= ThreeAddressCodeGenerator.Generate(ModifierSyntaxTree, SymbolsTable);
            }
        }

        public PostfixNotation PostfixNotation
        {
            get
            {
                return _postfixNotation ??= PostfixNotationGenerator.Generate(ModifierSyntaxTree, SymbolsTable);
            }
        }

        public Compiler(TextReader inputTextReader)
        {
            this._inputTextReader = inputTextReader;
        }


    }
}
