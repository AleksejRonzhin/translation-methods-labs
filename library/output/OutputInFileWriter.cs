namespace library.output
{
    public class OutputInFileWriter
    {
        protected static void WriteToFile(string text, string filename)
        {
            using var writer = new StreamWriter(filename, false);
            writer.Write(text);
        }
    }
}