using Newtonsoft.Json;

namespace ConsoleHttpClientDictionary18may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://dictionaryapi.dev/

            using var client = new HttpClient();

            string word = "hello";

            string requestUri = "https://api.dictionaryapi.dev/api/v2/entries/en/" + word;

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            var myJsonResponse = await response.Content.ReadAsStringAsync();

            //await Console.Out.WriteLineAsync(myJsonResponse);

            /*[{"word":"hello","phonetics":[{"audio":"https://api.dictionaryapi.dev/media/pronunciations/en/hello-au.mp3","sourceUrl":"https://commons.wikimedia.org/w/index.php?curid=75797336","license":{"name":"BY-SA 4.0","url":"https://creativecommons.org/licenses/by-sa/4.0"}},{"text":"/h?'l??/","audio":"https://api.dictionaryapi.dev/media/pronunciations/en/hello-uk.mp3","sourceUrl":"https://commons.wikimedia.org/w/index.php?curid=9021983","license":{"name":"BY 3.0 US","url":"https://creativecommons.org/licenses/by/3.0/us"}},{"text":"/h?'lo?/","audio":""}],"meanings":[{"partOfSpeech":"noun","definitions":[{"definition":"\"Hello!\" or an equivalent greeting.","synonyms":[],"antonyms":[]}],"synonyms":["greeting"],"antonyms":[]},{"partOfSpeech":"verb","definitions":[{"definition":"To greet with \"hello\".","synonyms":[],"antonyms":[]}],"synonyms":[],"antonyms":[]},{"partOfSpeech":"interjection","definitions":[{"definition":"A greeting (salutation) said when meeting someone or acknowledging someone's arrival or presence.","synonyms":[],"antonyms":[],"example":"Hello, everyone."},{"definition":"A greeting used when answering the telephone.","synonyms":[],"antonyms":[],"example":"Hello? How may I help you?"},{"definition":"A call for response if it is not clear if anyone is present or listening, or if a telephone conversation may have been disconnected.","synonyms":[],"antonyms":[],"example":"Hello? Is anyone there?"},{"definition":"Used sarcastically to imply that the person addressed or referred to has done something the speaker or writer considers to be foolish.","synonyms":[],"antonyms":[],"example":"You just tried to start your car with your cell phone. Hello?"},{"definition":"An expression of puzzlement or discovery.","synonyms":[],"antonyms":[],"example":"Hello! What's going on here?"}],"synonyms":[],"antonyms":["bye","goodbye"]}],"license":{"name":"CC BY-SA 3.0","url":"https://creativecommons.org/licenses/by-sa/3.0"},"sourceUrls":["https://en.wiktionary.org/wiki/hello"]}]*/

            // https://json2csharp.com/

            var myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);

            foreach (Root root in myDeserializedClass)
            {
                List<Meaning> meanings = root.meanings;
                foreach (Meaning meaning in meanings)
                {
                    List<Definition> definitions = meaning.definitions;
                    List<string> synonyms = meaning.synonyms;
                    List<string> antonyms = meaning.antonyms;
                    string partOfSpeech = meaning.partOfSpeech;

                    foreach (Definition definition in definitions)
                    {
                        var def = definition.definition;
                        await Console.Out.WriteLineAsync(def);
                    }
                }
            }

            /*
            "Hello!" or an equivalent greeting.
            To greet with "hello".
            A greeting (salutation) said when meeting someone or acknowledging someone's arrival or presence.
            A greeting used when answering the telephone.
            A call for response if it is not clear if anyone is present or listening, or if a telephone conversation may have been disconnected.
            Used sarcastically to imply that the person addressed or referred to has done something the speaker or writer considers to be foolish.
            An expression of puzzlement or discovery.*/

            Console.Read();
        }
    }
}
