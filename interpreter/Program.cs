using interpreter;
using library.compiler.core.models;
using System.Runtime.Serialization.Formatters.Binary;
internal class Program
{
    private static void Main(string[] args)
    {
        var binaryFileName = args[0];
        (string infixNotation, ThreeAddressCode threeAddressCode, SymbolsTable symbolsTable) = ReadBinaryFile(binaryFileName);

        Console.WriteLine(infixNotation);
        var interpeter = new Interpreter(threeAddressCode, symbolsTable);
        try
        {
            var result = interpeter.Start();
            Console.WriteLine($"Result: {result}");
        } catch (CalculatingException ex)
        {
            Console.WriteLine($"Ошибка вычисления! {ex.Message}");
        }
    }

    private static (string, ThreeAddressCode, SymbolsTable) ReadBinaryFile(string binaryFileName)
    {
        BinaryFormatter formatter = new();
        FileStream fileStream = new(binaryFileName, FileMode.Open);
        var infixNotation = (string)formatter.Deserialize(fileStream);
        var threeAddressCode = (ThreeAddressCode)formatter.Deserialize(fileStream);
        var symbolsTable = (SymbolsTable)formatter.Deserialize(fileStream);
        fileStream.Close();
        return (infixNotation, threeAddressCode, symbolsTable);
    }
}