namespace ConsoleHttpClient3feb2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://zetcode.com/csharp/httpclient/
            Console.WriteLine("HttpClient is a base class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI.");
            var url = "http://webcode.me";
            using var client = new HttpClient();

            var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

            Console.WriteLine(result);

            Console.Read();

            /*
StatusCode: 200, ReasonPhrase: 'OK', Version: 1.1, Content: System.Net.Http.HttpConnectionResponseContent, Headers:
{
  Server: nginx/1.18.0 (Ubuntu)
  Date: Fri, 09 Feb 2024 08:34:50 GMT
  Connection: keep-alive
  ETag: "64f33c9f-18b"
  Accept-Ranges: bytes
  Content-Type: text/html
  Content-Length: 395
  Last-Modified: Sat, 02 Sep 2023 13:46:07 GMT
}
            */
        }
    }
}
