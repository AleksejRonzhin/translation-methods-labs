using lab2.tokens;

namespace lab2
{
    internal class LexicalAnalyzer
    {
        internal void Analyze(TextReader textReader)
        { 
            int symbolCode = 0;
            char symbol = ' ';

            List<TokenCreator> creatorList = new()
            {
                new IdentifierTokenCreator(),
                new OperationTokenCreator(),
                // скобки
                // числа целые и вещественные
            };

            bool wasToken = false;
            while (symbolCode != -1)
            {
                wasToken = false;
                Console.WriteLine(symbol);

                creatorList.ForEach((creator) =>
                {

                });

                foreach(TokenCreator creator in creatorList)
                {
                    if (creator.Start(symbol))
                    {
                        //Console.WriteLine(symbol + " стартует");
                        symbolCode = textReader.Read();
                        symbol = (char)symbolCode;
                        while (symbolCode != 1 && creator.AddSymbol(symbol))
                        {
                            //Console.WriteLine(symbol + " продолжает");
                            symbolCode = textReader.Read();
                            symbol = (char)symbolCode;
                        }
                        //Console.WriteLine(symbol + " закончило");
                        string token = creator.GetToken();
                        Console.WriteLine("token:" + token);
                        wasToken = true;
                        break;
                       
                    }
                }
                if (wasToken) continue;

                if (symbol != ' ') throw new Exception();

                symbolCode = textReader.Read();
                symbol = (char)symbolCode;
            }
        }
    }
}
