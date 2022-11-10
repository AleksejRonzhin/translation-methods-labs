using library.compiler.core.models;

namespace library.output
{
    public class PostfixNotationInFileWriter : OutputInFileWriter
    {
        public static void WritePostfixNotation(string postfixNotationFilename, PostfixNotation postfixNotation)
        {

            WriteToFile(postfixNotation.ToString(), postfixNotationFilename);
        }
    }
}
