using System.Text;

namespace EntainAssignment
{
    public static class StringCompression
    {
        public static string CompressString(string? input)
        {
            if(string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            StringBuilder compressedString = new StringBuilder();
            char currentChar = input[0];
            int count = 1;
            for(int i = 1; i < input.Length; i++)
            {
                if (input[i] == currentChar)
                {
                    count++;
                }
                else
                {
                    compressedString.Append(count);
                    compressedString.Append(currentChar);
                    currentChar = input[i];
                    count = 1;
                }               
            }
            compressedString.Append(count);
            compressedString.Append(currentChar);
            return compressedString.ToString();
        }
    }
}
