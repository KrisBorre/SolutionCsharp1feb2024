using LibraryDatabaseDictionary25may2024;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDictionary27may2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DictionaryDbContext24may2024 dbContext = new DictionaryDbContext24may2024();

            List<string> list = new List<string>();

            Console.WriteLine("The 7 habits by Stephen Covey");

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
            list.Add("value");
            list.Add("driven");

            // page 79
            Console.WriteLine("The ability to subordinate an impulse to a value is the essence of the proactive person.");
            list.Add("subordinate");
            list.Add("impulse");
            list.Add("essence");

            Console.WriteLine("Proactive people are driven by values - carefully thought about, selected, and internalized values.");
            //list.Add("thought"); // Not found
            list.Add("selected");
            list.Add("internalized");

            Console.WriteLine("Use your resourcefulness and initiative.");
            list.Add("resourcefulness");
            list.Add("initiative");

            Console.WriteLine("Proactive language:");
            Console.WriteLine("Let's look at our alternatives.");
            list.Add("alternatives");

            Console.WriteLine("I can choose a different approach.");
            list.Add("different");
            list.Add("approach");


            foreach (string word in list)
            {
                var columnName1 = "Word";
                var columnValue1 = new SqliteParameter("columnValue", word);

                string sql4 = $"SELECT * FROM Words WHERE {columnName1} = @columnValue;";
                var list4 = dbContext.Words.FromSqlRaw(sql4, columnValue1).ToList();

                if (list4.Count == 1)
                {
                    EnglishWord englishWord = list4[0];
                    Console.WriteLine("Word: " + englishWord.Word);
                    int wordId = englishWord.WordID;

                    var columnName2 = "WordId";
                    var columnValue2 = new SqliteParameter("columnValue", wordId);

                    string sql5 = $"SELECT * FROM Meanings WHERE {columnName2} = @columnValue";
                    List<EnglishMeaning> list5 = dbContext.Meanings.FromSqlRaw(sql5, columnValue2).ToList();
                    foreach (EnglishMeaning meaning in list5)
                    {
                        Console.WriteLine("Part of speech: " + meaning.PartOfSpeech);
                        englishWord.Meanings.Add(meaning);

                        string sql6 = $"SELECT * FROM Definitions WHERE MeaningID = (SELECT MeaningID FROM Meanings WHERE {columnName2} = @columnValue);";
                        List<EnglishDefinition> list6 = dbContext.Definitions.FromSqlRaw(sql6, columnValue2).ToList();
                        foreach (EnglishDefinition definition in list6)
                        {
                            if (meaning.MeaningID == definition.MeaningID)
                            {
                                meaning.Definitions.Add(definition);
                                Console.WriteLine("Definition: " + definition.Definition);
                                Console.WriteLine("Example: " + definition.Example);
                            }
                        }

                        string sql7 = $"SELECT * FROM Synonyms WHERE MeaningID = (SELECT MeaningID FROM Meanings WHERE {columnName2} = @columnValue);";
                        List<EnglishSynonym> list7 = dbContext.Synonyms.FromSqlRaw(sql7, columnValue2).ToList();
                        foreach (EnglishSynonym synonym in list7)
                        {
                            if (meaning.MeaningID == synonym.MeaningID)
                            {
                                meaning.Synonyms.Add(synonym);
                                Console.WriteLine("Synonym: " + synonym.Synonym);
                            }
                        }

                        string sql8 = $"SELECT * FROM Antonyms WHERE MeaningID = (SELECT MeaningID FROM Meanings WHERE {columnName2} = @columnValue);";
                        List<EnglishAntonym> list8 = dbContext.Antonyms.FromSqlRaw(sql8, columnValue2).ToList();
                        foreach (EnglishAntonym antonym in list8)
                        {
                            if (meaning.MeaningID == antonym.MeaningID)
                            {
                                meaning.Antonyms.Add(antonym);
                                Console.WriteLine("Antonym: " + antonym.Antonym);
                            }
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine();
                }

            }


            Console.Read();
        }
    }
}
