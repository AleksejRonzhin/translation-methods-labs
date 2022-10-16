namespace library.lexis
{
    internal class PositionTextReader
    {
        private readonly TextReader _textReader;
        private int position = 0;

        public PositionTextReader(TextReader textReader)
        {
            _textReader = textReader;
        }

        public (int position, char symbol, bool isLast) Read()
        {
            int symbolCode = this._textReader.Read();
            return (position++, (char)symbolCode, symbolCode == -1);
        }
    }
}
