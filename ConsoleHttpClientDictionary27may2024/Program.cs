using LibraryDatabaseDictionary25may2024;
using LibraryHttpClientDictionary26may2024;
using Newtonsoft.Json;

namespace ConsoleHttpClientDictionary27may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("The 7 habits by Stephen Covey");

            List<string> list = new List<string>();
            // page 17
            Console.WriteLine("We must look at the lens through which we see the world, as well as at the world we see, and that the lens itself shapes how we interpret the world");
            //list.Add("look"); // Not found
            //list.Add("lens");
            //list.Add("through");
            //list.Add("world");
            //list.Add("see");
            //list.Add("shapes");
            //list.Add("interpret");

            // page 32
            Console.WriteLine("Paradigms are inseparable from character.");
            Console.WriteLine("Being is seeing in the human dimension.");
            Console.WriteLine("What we see is highly interrelated to what we are.");
            //list.Add("interrelated");

            Console.WriteLine("The way we see the problem is the problem.");
            //list.Add("way");
            //list.Add("problem");

            Console.WriteLine("If you want to have a happy marriage, be the kind of person who generates positive energy and sidesteps negative energy rather than empowering it.");
            //list.Add("happy");
            //list.Add("marriage");
            //list.Add("generates");
            //list.Add("energy");
            //list.Add("sidesteps");
            //list.Add("empowering");

            Console.WriteLine("Be more understanding, empathic, consistent, loving.");
            //list.Add("understanding");
            //list.Add("empathic");
            //list.Add("consistent");
            //list.Add("loving");



            // page 78
            Console.WriteLine("Look at the word responsibility - response ability - the ability to choose your response.");
            //list.Add("responsibility");
            //list.Add("ability");
            //list.Add("choose");
            //list.Add("response");

            Console.WriteLine("Highly proactive people recognize that responsibility.");
            //list.Add("proactive");
            //list.Add("recognize");

            Console.WriteLine("Proactive people do not blame circumstances, conditions, or conditioning for their behavior.");
            //list.Add("blame");
            //list.Add("circumstances");
            //list.Add("conditions");
            //list.Add("conditioning");
            //list.Add("behavior");

            Console.WriteLine("Proactive people can carry their own weather with them.");
            Console.WriteLine("Whether it rains or shines makes no difference to proactive people.");
            Console.WriteLine("Proactive people are value driven.");
            //list.Add("value");
            //list.Add("driven");

            // page 79
            Console.WriteLine("The ability to subordinate an impulse to a value is the essence of the proactive person.");
            //list.Add("subordinate");
            //list.Add("impulse");
            //list.Add("essence");

            Console.WriteLine("Proactive people are driven by values - carefully thought about, selected, and internalized values.");
            //list.Add("thought"); // Not found
            //list.Add("selected");
            //list.Add("internalized");

            Console.WriteLine("Use your resourcefulness and initiative.");
            //list.Add("resourcefulness");
            //list.Add("initiative");

            Console.WriteLine("Proactive language:");
            Console.WriteLine("Let's look at our alternatives.");
            //list.Add("alternatives");

            Console.WriteLine("I can choose a different approach.");
            //list.Add("different");
            //list.Add("approach");

            DictionaryDbContext24may2024 dbContext = new DictionaryDbContext24may2024();
            dbContext.Database.EnsureCreated();

            // https://dictionaryapi.dev/

            for (int l = 0; l < list.Count; l++)
            {
                string word = list[l];

                Console.WriteLine("word = " + word);
                List<EnglishWord> words = dbContext.Words.ToList();
                bool alreadyExists = false;
                foreach (EnglishWord englishWord in words)
                {
                    if (englishWord.Word == word)
                    {
                        alreadyExists = true;
                        Console.WriteLine(word + " already exists in database!");
                    }
                }

                if (!alreadyExists)
                {
                    bool isCallingApi = true;

                    string myJsonResponse;

                    if (isCallingApi)
                    {
                        using var client = new HttpClient();
                        string requestUri = "https://api.dictionaryapi.dev/api/v2/entries/en/" + word;
                        HttpResponseMessage response = await client.GetAsync(requestUri);
                        response.EnsureSuccessStatusCode();
                        myJsonResponse = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        word = "hello";
                        myJsonResponse = "[{\"word\":\"hello\",\"phonetics\":[{\"audio\":\"https://api.dictionaryapi.dev/media/pronunciations/en/hello-au.mp3\",\"sourceUrl\":\"https://commons.wikimedia.org/w/index.php?curid=75797336\",\"license\":{\"name\":\"BY-SA 4.0\",\"url\":\"https://creativecommons.org/licenses/by-sa/4.0\"}},{\"text\":\"/h?'l??/\",\"audio\":\"https://api.dictionaryapi.dev/media/pronunciations/en/hello-uk.mp3\",\"sourceUrl\":\"https://commons.wikimedia.org/w/index.php?curid=9021983\",\"license\":{\"name\":\"BY 3.0 US\",\"url\":\"https://creativecommons.org/licenses/by/3.0/us\"}},{\"text\":\"/h?'lo?/\",\"audio\":\"\"}],\"meanings\":[{\"partOfSpeech\":\"noun\",\"definitions\":[{\"definition\":\"\\\"Hello!\\\" or an equivalent greeting.\",\"synonyms\":[],\"antonyms\":[]}],\"synonyms\":[\"greeting\"],\"antonyms\":[]},{\"partOfSpeech\":\"verb\",\"definitions\":[{\"definition\":\"To greet with \\\"hello\\\".\",\"synonyms\":[],\"antonyms\":[]}],\"synonyms\":[],\"antonyms\":[]},{\"partOfSpeech\":\"interjection\",\"definitions\":[{\"definition\":\"A greeting (salutation) said when meeting someone or acknowledging someone's arrival or presence.\",\"synonyms\":[],\"antonyms\":[],\"example\":\"Hello, everyone.\"},{\"definition\":\"A greeting used when answering the telephone.\",\"synonyms\":[],\"antonyms\":[],\"example\":\"Hello? How may I help you?\"},{\"definition\":\"A call for response if it is not clear if anyone is present or listening, or if a telephone conversation may have been disconnected.\",\"synonyms\":[],\"antonyms\":[],\"example\":\"Hello? Is anyone there?\"},{\"definition\":\"Used sarcastically to imply that the person addressed or referred to has done something the speaker or writer considers to be foolish.\",\"synonyms\":[],\"antonyms\":[],\"example\":\"You just tried to start your car with your cell phone. Hello?\"},{\"definition\":\"An expression of puzzlement or discovery.\",\"synonyms\":[],\"antonyms\":[],\"example\":\"Hello! What's going on here?\"}],\"synonyms\":[],\"antonyms\":[\"bye\",\"goodbye\"]}],\"license\":{\"name\":\"CC BY-SA 3.0\",\"url\":\"https://creativecommons.org/licenses/by-sa/3.0\"},\"sourceUrls\":[\"https://en.wiktionary.org/wiki/hello\"]}]";
                    }

                    // https://json2csharp.com/

                    var myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);

                    int number_of_roots = myDeserializedClass.Count;
                    Console.WriteLine("number of roots = " + number_of_roots);
                    for (int i = 0; i < number_of_roots; i++)
                    {
                        Console.WriteLine("Root " + (i + 1));
                        Root root = myDeserializedClass[i];

                        EnglishWord englishWord = new EnglishWord();
                        englishWord.Word = word;
                        var wordEntry = dbContext.Words.Add(englishWord);
                        dbContext.SaveChanges();
                        int wordId = englishWord.WordID;

                        List<Meaning> meanings = root.meanings;
                        int number_of_meanings = meanings.Count;
                        Console.WriteLine("number of meanings = " + number_of_meanings);

                        for (int j = 0; j < number_of_meanings; j++)
                        {
                            Console.WriteLine("Meaning " + (j + 1));
                            Meaning meaning = meanings[j];

                            string partOfSpeech = meaning.partOfSpeech;
                            Console.WriteLine("part of speech = " + partOfSpeech);

                            EnglishMeaning englishMeaning = new EnglishMeaning();
                            englishMeaning.WordID = wordId;
                            englishMeaning.PartOfSpeech = partOfSpeech;
                            var meaningEntry = dbContext.Meanings.Add(englishMeaning);
                            dbContext.SaveChanges();
                            int meaningId = englishMeaning.MeaningID;

                            List<string> synonyms = meaning.synonyms;
                            int number_of_synonyms = synonyms.Count;
                            Console.WriteLine("number of synonyms = " + number_of_synonyms);
                            List<object> synonumEntries = new List<object>();
                            foreach (string synonym in synonyms)
                            {
                                Console.WriteLine("synonym = " + synonym);
                                EnglishSynonym englishSynonym = new EnglishSynonym();
                                englishSynonym.MeaningID = meaningId;
                                englishSynonym.Synonym = synonym;
                                var synonymEntry = dbContext.Synonyms.Add(englishSynonym);
                                synonumEntries.Add(synonymEntry);
                                dbContext.SaveChanges();
                            }

                            List<string> antonyms = meaning.antonyms;
                            int number_of_antonyms = antonyms.Count;
                            Console.WriteLine("number of antonyms = " + number_of_antonyms);
                            List<object> antonymEntries = new List<object>();
                            foreach (string antonym in antonyms)
                            {
                                Console.WriteLine("antonym = " + antonym);
                                EnglishAntonym englishAntonym = new EnglishAntonym();
                                englishAntonym.MeaningID = meaningId;
                                englishAntonym.Antonym = antonym;
                                var antonymEntry = dbContext.Antonyms.Add(englishAntonym);
                                antonymEntries.Add(antonymEntry);
                                dbContext.SaveChanges();
                            }

                            List<Definition> definitions = meaning.definitions;
                            int number_of_definitions = definitions.Count;

                            for (int k = 0; k < number_of_definitions; k++)
                            {
                                Console.WriteLine("Definition " + (k + 1));
                                Definition definition = definitions[k];
                                string def = definition.definition;
                                Console.WriteLine("definition = " + def);

                                string example = definition.example;
                                Console.WriteLine("example = " + example);

                                EnglishDefinition englishDefinition = new EnglishDefinition();
                                englishDefinition.MeaningID = meaningId;
                                englishDefinition.Definition = def;
                                englishDefinition.Example = example;
                                var definitionEntry = dbContext.Definitions.Add(englishDefinition);
                                dbContext.SaveChanges();
                            }

                        }
                    }
                }

            }


            Console.Read();
        }
    }
}
