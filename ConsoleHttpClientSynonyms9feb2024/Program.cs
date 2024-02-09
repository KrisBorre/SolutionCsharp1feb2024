namespace ConsoleHttpClientSynonyms9feb2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://www.synonyms.com/synonyms_api.php

            using var client = new HttpClient();

            string word = "consistent";

            string requestUri = @$"https://www.stands4.com/services/v2/syno.php?uid=1001&tokenid=tk324324&word={word}&format=xml";

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var resp = await response.Content.ReadAsStringAsync();

            await Console.Out.WriteLineAsync(resp);

            // Invalid user

            /*
            <?xml version="1.0" encoding="UTF-8"?>
            <results><error>Invalid User ID</error></results>
            */

            Console.Read();
        }
    }
}
