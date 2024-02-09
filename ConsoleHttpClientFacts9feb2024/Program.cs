namespace ConsoleHttpClientFacts9feb2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://uselessfacts.jsph.pl/

            using var client = new HttpClient();

            string requestUri = "https://uselessfacts.jsph.pl/api/v2/facts/random?language=en";

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var resp = await response.Content.ReadAsStringAsync();

            await Console.Out.WriteLineAsync(resp);

            /*{"id":"3a061e7a74a910de2a85fc3faaceca42","text":"You can be fined up to $1,000 for whistling on Sunday in Salt Lake City, Utah.","source":"djtech.net","source_url":"http://www.djtech.net/humor/useless_facts.htm","language":"en","permalink":"https://uselessfacts.jsph.pl/api/v2/facts/3a061e7a74a910de2a85fc3faaceca42"}*/

            /*{"id":"a5e575be097f8479e8f818b8461dae00","text":"Over 1000 birds a year die from smashing into windows!","source":"djtech.net","source_url":"http://www.djtech.net/humor/useless_facts.htm","language":"en","permalink":"https://uselessfacts.jsph.pl/api/v2/facts/a5e575be097f8479e8f818b8461dae00"}*/

            Console.Read();
        }
    }
}
