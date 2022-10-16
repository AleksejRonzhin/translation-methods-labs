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
    }
}
