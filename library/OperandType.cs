namespace library
{
    public enum OperandType
    {
        INTEGER,
        REAL,
        ANY
    }

    public static class OperandTypeUtils
    {
        public static string ConvertToText(this OperandType type)
        {
            return type switch
            {
                OperandType.INTEGER => "целое",
                OperandType.REAL => "вещественное",
                _ => ""
            };
        }

        public static OperandType GetByLine(string line)
        {
            return line switch
            {
                "i" or "I" => OperandType.INTEGER,
                "f" or "F" => OperandType.REAL,
                _ => throw new Exception()
            };
        }
    }
}
