using System.Text.Json;

namespace ConsoleHttpClientDisney17may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://disneyapi.dev/docs/

            using var client = new HttpClient();

            string requestUri = "https://api.disneyapi.dev/character?name=Bambi";

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var resp = await response.Content.ReadAsStringAsync();

            //await Console.Out.WriteLineAsync(resp);

            Rootobject rootobject = JsonSerializer.Deserialize<Rootobject>(resp);

            // Create empty file in VS -> Paste Special -> Paste JSON as class

            Info info = rootobject.info;

            Datum[] datum = rootobject.data;

            int count = info.count;
            await Console.Out.WriteLineAsync("count = " + count);

            int length = datum.Length;
            await Console.Out.WriteLineAsync("datum length = " + length);

            var allies = datum[0].allies;

            await Console.Out.WriteLineAsync("allies = " + allies);

            string name = datum[0].name;

            await Console.Out.WriteLineAsync("name = " + name);

            /*
            count = 2
            datum length = 2
            allies = System.Object[]
            name = Bambi
            */

            Console.Read();
        }
    }


    public class Rootobject
    {
        public Info info { get; set; }
        public Datum[] data { get; set; }
    }

    public class Info
    {
        public int count { get; set; }
        public int totalPages { get; set; }
        public object previousPage { get; set; }
        public object nextPage { get; set; }
    }

    public class Datum
    {
        public int _id { get; set; }
        public string[] films { get; set; }
        public string[] shortFilms { get; set; }
        public string[] tvShows { get; set; }
        public string[] videoGames { get; set; }
        public string[] parkAttractions { get; set; }
        public object[] allies { get; set; }
        public object[] enemies { get; set; }
        public string sourceUrl { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string url { get; set; }
        public int __v { get; set; }
    }
}
