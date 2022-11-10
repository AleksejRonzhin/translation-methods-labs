namespace library.compiler.core.models
{
    public class Operation
    {
        public static readonly List<char> ValidSymbols = new() { '+', '-', '*', '/', '=' };

        public static List<Operation> Operations { get; } = new()
        {
            new Operation("+", "операция сложения", true, true, 2, "add"),
            new Operation("-", "операция вычитания", false, true, 2, "sub"),
            new Operation("*", "операция умножения", true, true, 3, "mul"),
            new Operation("/", "операция деления", false, true, 3, "div"),
            new Operation("=", "операция присваивания", false, false, 1, "_")
        };

        public static int MaxPrioritet = Operations.Max(op => op.Prioritet);
        public static int MinPrioritet = Operations.Min(op => op.Prioritet);

        public string Sign { get; }
        public bool IsAssociative { get; }
        public bool IsDirectProcedure { get; } // Слева направо
        public int Prioritet { get; }
        public string Name { get; }
        public string Code { get; }

        public Operation(string sign, string name, bool isAssociative, bool isDirectProcedure, int prioritet, string code)
        {
            Sign = sign;
            IsAssociative = isAssociative;
            IsDirectProcedure = isDirectProcedure;
            Prioritet = prioritet;
            Name = name;
            Code = code;
        }
    }
}
