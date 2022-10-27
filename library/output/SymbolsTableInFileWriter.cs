using System.Text;

namespace library.output
{
    public class SymbolsTableInFileWriter : OutputInFileWriter
    {
        public static void WriteSymbolsTable(string symbolsTableFilename, SymbolsTable table)
        {
            StringBuilder stringBuilder = new();
            foreach (KeyValuePair<string, int> entry in table.Symbols)
            {
                stringBuilder.AppendLine($"{entry.Value} - {entry.Key}");
            }
            WriteToFile(stringBuilder.ToString(), symbolsTableFilename);
        }

    }
}
