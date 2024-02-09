namespace ConsoleHttpClient5feb2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://zetcode.com/csharp/httpclient/
            Console.WriteLine("HttpClient is a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.");

            Console.WriteLine("Query string is a part of the URL which is used to add some data to the request for the resource. It is often a sequence of key/value pairs. It follows the path and starts with the ? character.");

            var u = "http://webcode.me/qs.php";

            using var client = new HttpClient();

            Console.WriteLine("Currently, the http request times out after 100 s. To set a different timeout, we can use the TimeOut property.");

            client.Timeout = TimeSpan.FromMinutes(3);

            var builder = new UriBuilder(u);
            builder.Query = "name=John Doe&occupation=gardener";
            var url = builder.ToString();

            var res = await client.GetAsync(url);

            var content = await res.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            /*echo json_encode($_GET);*/

            Console.Read();
        }
    }
}
