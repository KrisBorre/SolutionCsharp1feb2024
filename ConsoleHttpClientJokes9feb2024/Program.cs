namespace ConsoleHttpClientJokes9feb2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://humorapi.com/docs/

            using var client = new HttpClient();

            string requestUri = "https://api.humorapi.com/jokes/search?number=3&keywords=horse,man";

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            /*
            System.Net.Http.HttpRequestException
            HResult=0x80131500
            Message=Response status code does not indicate success: 401 (Unauthorized).
            Source=System.Net.Http
            */

            var resp = await response.Content.ReadAsStringAsync();

            await Console.Out.WriteLineAsync(resp);

            Console.Read();
        }
    }
}
