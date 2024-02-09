namespace ConsoleHttpClient8feb2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://zetcode.com/csharp/httpclient/
            Console.WriteLine("HttpClient is a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.");

            Console.WriteLine("C# HttpClient download image");

            Console.WriteLine("The GetByteArrayAsync sends a GET request to the specified Uri and returns the response body as a byte array in an asynchronous operation.");

            using var httpClient = new HttpClient();

            var url = "http://webcode.me/favicon.ico";
            byte[] imageBytes = await httpClient.GetByteArrayAsync(url);
            /*
            System.Net.Http.HttpRequestException
            HResult = 0x80131500
            Message = Response status code does not indicate success: 404 (Not Found).
            Source = System.Net.Http
            */

            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            string localFilename = "favicon.ico";
            string localPath = Path.Combine(documentsPath, localFilename);

            Console.WriteLine(localPath);
            File.WriteAllBytes(localPath, imageBytes);

            Console.Read();
        }
    }
}
