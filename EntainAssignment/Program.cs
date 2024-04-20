namespace EntainAssignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            //string compression
            Console.WriteLine("Enter the string to compress:");
            var input = Console.ReadLine();
            string compressedString = StringCompression.CompressString(input);
            Console.WriteLine(compressedString);

            // multiple download
            var urls = new List<string>
            {
                "https://example.com",
                "https://google.com",
                "https://youtube.com"
                // add more urls as needed
            };
            var output = AsyncDownload.DownloadUrlAsync1(urls, 2);
            foreach (var item in output.Result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
