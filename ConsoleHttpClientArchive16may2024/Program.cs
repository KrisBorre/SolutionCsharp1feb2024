using System.Text.Json;

namespace ConsoleHttpClientArchive16may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://archive.org/help/aboutsearch.htm

            using var client = new HttpClient();

            string requestUri = "https://archive.org/advancedsearch.php?q=subject:palm+pilot+software&output=json&rows=100&page=5";

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var resp = await response.Content.ReadAsStringAsync();

            //await Console.Out.WriteLineAsync(resp);

            // Create empty file in VS -> Paste Special -> Paste JSON as class

            Rootobject rootobject = JsonSerializer.Deserialize<Rootobject>(resp);

            Responseheader header = rootobject.responseHeader;
            Response object_response = rootobject.response;

            Doc[] docs = object_response.docs;

            int length = docs.Length;

            await Console.Out.WriteLineAsync("length=" + length);

            Doc doc1 = docs[0];

            await Console.Out.WriteLineAsync("title = " + doc1.title);

            await Console.Out.WriteLineAsync("description = " + doc1.description);

            /*
            length=100
            title = AstroWorld Suite
            description = This astrology software will produce horoscopes and foretell astrological daily forecasts. 
            It offers both text and graphical outputs for easy reading. 
            This program is for Handheld PCs with a MIPS processor. Register this program if you want a daily forecast.
            */

            Console.Read();
        }
    }
}
