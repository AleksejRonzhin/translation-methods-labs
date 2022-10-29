using System.Text;

namespace library
{
    public class SymbolsTable
    {
        public SymbolsTable()
        {
            this.Symbols = new();
        }

        public Dictionary<string, int> Symbols { get; } 

        public int Add(string symbol)
        {
            if (Symbols.ContainsKey(symbol)) return Symbols[symbol];
            int id = Symbols.Count + 1;
            Symbols.Add(symbol, id);
            return id;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            foreach (KeyValuePair<string, int> entry in Symbols)
            {
                stringBuilder.AppendLine($"{entry.Value} - {entry.Key}");
            }
            return stringBuilder.ToString();
        }
    }
}
