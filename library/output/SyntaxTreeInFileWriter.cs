using library.compiler.syntax.tree;

namespace library.output
{
    public class SyntaxTreeInFileWriter : OutputInFileWriter
    {
        public static void WriteSyntaxTree(string syntaxTreeFilename, SyntaxTree syntaxTree)
        {
            WriteToFile(SyntaxTreePrinter.Print(syntaxTree), syntaxTreeFilename);
        }

    }
}
