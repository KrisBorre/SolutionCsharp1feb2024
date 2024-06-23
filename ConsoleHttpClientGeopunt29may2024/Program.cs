using Newtonsoft.Json;

namespace ConsoleHttpClientGeopunt29may2024
{
    internal class Program
    {
        private const string BaseUrl = "https://geo.api.vlaanderen.be/geolocation/v4/suggestion";
        private const string QueryParamName = "q";

        static async Task Main(string[] args)
        {
            // https://www.geopunt.be/
            using var client = new HttpClient();

            //string query = "Kasteeldreef 137, 2970 Schilde";
            //string query = "de";
            string query = "Veld";
            //string query = "Trambergstraat";

            // Build the URL with query parameters
            var url = $"{BaseUrl}?{QueryParamName}={query}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                var responseString = await response.Content.ReadAsStringAsync();

                // string query = "Trambergstraat";
                /*"{\"SuggestionResult\":[\"Trambergstraat, Zonhoven\"]}"*/

                // string query = "Kasteeldreef 137, 2970 Schilde";
                // "{\"SuggestionResult\":[\"Kasteeldreef 137, 2970 Schilde\"]}"

                // string query = "Veld";
                // "{\"SuggestionResult\":[\"Assenede\",\"Evergem\",\"Lanaken\",\"Lichtervelde\",\"Lochristi\"]}"

                Console.WriteLine(responseString);

                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseString);

                Console.WriteLine("myDeserializedClass.SuggestionResult = ");
                foreach (string suggestion in myDeserializedClass.SuggestionResult)
                {
                    Console.WriteLine(suggestion);
                }

                /*
                Assenede
                Evergem
                Lanaken
                Lichtervelde
                Lochristi
                */
            }


            Console.ReadLine();
        }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Root
    {
        public List<string> SuggestionResult { get; set; }
    }
}

