using library.compiler.core.models;

namespace library.output
{
    public class ThreeAddressCodeInFileWriter : OutputInFileWriter
    {
        public static void WriteThreeAddressCode(string threeAddressCodeFilename, ThreeAddressCode code)
        {

            WriteToFile(code.ToString(), threeAddressCodeFilename);
        }
    }
}
