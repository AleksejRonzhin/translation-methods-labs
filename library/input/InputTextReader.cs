namespace library.input
{
    public class InputTextReader
    {
        public static string ReadTextFromFile(string filename)
        {
            using var reader = new StreamReader(filename);
            return reader.ReadToEnd();
        }
    }
}
