using library.compiler.core.tokens;
using System.Text;

namespace library.output
{
    public class TokensInFileWriter : OutputInFileWriter
    {
        public static void WriteTokens(string tokensFilename, List<TokenInfo> tokens)
        {
            StringBuilder stringBuilder = new();
            tokens.ForEach(token =>
            {
                stringBuilder.AppendLine(token.ToString());
            });
            WriteToFile(stringBuilder.ToString(), tokensFilename);
        }
    }
}
