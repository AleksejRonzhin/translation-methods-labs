namespace library.output
{
    public class PostfixNotationInFileWriter : OutputInFileWriter
    {
        public static void WritePostfixNotation(string postfixNotationFilename, string postfixNotation)
        {

            WriteToFile(postfixNotation, postfixNotationFilename);
        }
    }
}
