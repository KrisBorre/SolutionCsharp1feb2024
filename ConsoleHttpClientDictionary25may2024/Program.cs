﻿using LibraryDatabaseDictionary25may2024;
using Newtonsoft.Json;

namespace ConsoleHttpClientDictionary25may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://dictionaryapi.dev/

            // The structure of scientific revolutions by Thomas S. Kuhn.

            List<string> list = new List<string>();
            //list.Add("adamant");
            //list.Add("school of thought");
            //list.Add("perspicuous");
            //list.Add("moribund");
            //list.Add("avocation");
            ////list.Add("avocational"); // Not found
            //list.Add("verisimilitude");
            //list.Add("viability");
            //list.Add("overt");
            //list.Add("congeries"); // page 2
            //list.Add("accidents"); // page 4
            //list.Add("epistemology"); // page 8
            //list.Add("esoteric"); // page 12
            //list.Add("arduous"); // page 15
            //list.Add("recondite"); // page 15
            //list.Add("chaff"); // page 16
            //list.Add("juxtapose"); // page 16
            //list.Add("inchoate"); // page 17
            //list.Add("unequivocal"); // page 21
            //list.Add("preconception"); // page 39
            ////list.Add("penumbral"); // page 43 // Not found
            //list.Add("embroidery"); // page 47
            //list.Add("ramshackle"); // page 49
            //list.Add("askew"); // page 54
            //list.Add("acidity"); // page 55
            //list.Add("caloric"); // page 55
            //list.Add("roasting"); // page 71
            //list.Add("retention"); // page 72
            //list.Add("concomitant"); // page 75
            //// list.Add("enunciation"); // page 75 // Not found
            //list.Add("tautologies"); // page 78
            //list.Add("indictment"); // page 81
            //list.Add("purported"); // page 102
            //list.Add("inextricable"); // page 109
            //list.Add("recourse"); // page 110           
            //list.Add("concomitants"); // page 112
            //list.Add("equivocal"); // page 116
            //list.Add("inundates"); // page 122
            ////list.Add("incommensurability"); // page 147 // Not found

            DictionaryDbContext24may2024 dbContext = new DictionaryDbContext24may2024();
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

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
                    bool isCallingApi = true; // false; // true;

                    string myJsonResponse;

                    if (isCallingApi)
                    {
                        using var client = new HttpClient();
                        string requestUri = "https://api.dictionaryapi.dev/api/v2/entries/en/" + word;
                        HttpResponseMessage response = await client.GetAsync(requestUri);
                        response.EnsureSuccessStatusCode();
                        myJsonResponse = await response.Content.ReadAsStringAsync();

                        //await Console.Out.WriteLineAsync(myJsonResponse);
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
