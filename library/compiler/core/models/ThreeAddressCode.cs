using System.Text;

namespace library.compiler.core.models
{
    [Serializable]
    public class ThreeAddressCode 
    {
        public List<ThreeAddressLine> Lines
        {
            get;
        }

        public ThreeAddressCode()
        {
            Lines = new();
        }

        public void AddLine(ThreeAddressLine line)
        {
            Lines.Add(line);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            Lines.ForEach(line =>
            {
                stringBuilder.AppendLine(line.ToString());
            });
            return stringBuilder.ToString();
        }
    }
}
