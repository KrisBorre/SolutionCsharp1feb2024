using Newtonsoft.Json;

namespace ConsoleHttpClientDisney18may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://disneyapi.dev/docs/

            using var client = new HttpClient();
            string name = "Bambi";

            string requestUri = "https://api.disneyapi.dev/character?name=" + name;

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var myJsonResponse = await response.Content.ReadAsStringAsync();

            //await Console.Out.WriteLineAsync(myJsonResponse);

            // https://json2csharp.com/

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            List<Datum> data = myDeserializedClass.data;

            foreach (Datum item in data)
            {
                List<string> films = item.films;
                List<object> allies = item.allies;
                List<object> enemies = item.enemies;

                foreach (string film in films)
                {
                    await Console.Out.WriteLineAsync(film);
                }
            }

            /*
            Bambi (film)
            Bambi II
            Perri
            The Rescuers
            Who Framed Roger Rabbit
            Mickey's Magical Christmas: Snowed in at the House of Mouse
            A Goofy Movie
            The Reluctant Dragon
            Zootopia
            Ralph Breaks the Internet
            Bambi (live-action film)
            Bambi (film)
            The Sword in the Stone
            The Jungle Book
            The Rescuers
            Who Framed Roger Rabbit
            Beauty and the Beast (1991 film)
            Bambi II
            */

            Console.Read();
        }
    }
}
