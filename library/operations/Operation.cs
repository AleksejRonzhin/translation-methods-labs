namespace library.operations
{
    public class Operation
    {
        public static List<char> ValidSymbols = new() { '+', '-', '*', '/', '=' };

        public static List<Operation> Operations { get; } = new()
        {
            new Operation("+", "операция сложения", true, true, 2),
            new Operation("-", "операция вычитания", false, true, 2),
            new Operation("*", "операция умножения", true, true, 3),
            new Operation("/", "операция деления", false, true, 3),
            new Operation("=", "операция присваивания", false, false, 1)
        };

        public static int MaxPrioritet = Operations.Max(op => op.Prioritet);
        public static int MinPrioritet = Operations.Min(op => op.Prioritet);

        public string Sign { get; }
        public bool IsAssociative { get; }
        public bool IsDirectProcedure { get; } // Слева направо
        public int Prioritet { get; }
        public string Name { get; }

        public Operation(string sign, string name, bool isAssociative, bool isDirectProcedure, int prioritet)
        {
            Sign = sign;
            IsAssociative = isAssociative;
            IsDirectProcedure = isDirectProcedure;
            Prioritet = prioritet;
            Name = name;
        }
    }
}
