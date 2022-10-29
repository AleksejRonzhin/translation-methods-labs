using System.Text;

namespace library.output
{
    public class SymbolsTableInFileWriter : OutputInFileWriter
    {
        public static void WriteSymbolsTable(string symbolsTableFilename, SymbolsTable table)
        {
            
            WriteToFile(table.ToString(), symbolsTableFilename);
        }

    }
}
