using library.compiler.core.models;
using System.Runtime.Serialization.Formatters.Binary;
var binaryFileName = "post_code.bin";

BinaryFormatter formatter = new();
FileStream fileStream = new(binaryFileName, FileMode.Open);

var threeAddressCode = (ThreeAddressCode)formatter.Deserialize(fileStream);
var symbolsTable = (SymbolsTable)formatter.Deserialize(fileStream);
fileStream.Close();

Console.WriteLine(threeAddressCode);
Console.WriteLine(symbolsTable);

Console.WriteLine(symbolsTable.Symbols);

symbolsTable.Symbols.ForEach(symbol =>
{
    if (!symbol.IsTemp)
    {
        Console.WriteLine($"input {symbol.Name} {symbol.OperandType}");
        var input = Console.ReadLine();
        symbol.Value = input;
        Console.WriteLine(symbol.Value);
    }
}); 
