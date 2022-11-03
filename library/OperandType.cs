namespace library
{
    public enum OperandType
    {
        INTEGER,
        REAL,
        ANY,
        NOT_DEFINED
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
                _ => OperandType.NOT_DEFINED
            };
        }
    }
}
