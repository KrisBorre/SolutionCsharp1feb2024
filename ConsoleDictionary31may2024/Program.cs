using LibraryDatabaseDictionary25may2024;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDictionary31may2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DictionaryDbContext24may2024 dbContext = new DictionaryDbContext24may2024();

            List<string> list = new List<string>();

            Console.WriteLine("Write No Matter What: Advice for Academics by Joli Jensen");
            //list.Add("uncongenial");
            //// page 43
            //list.Add("stalled");
            //list.Add("stall");
            //// page 58
            //list.Add("heirloom");
            //// page 59
            //list.Add("respite");
            //// page 60
            //list.Add("snide");
            //list.Add("impervious");
            //// page 65
            //list.Add("enviable");
            //// page 67
            //list.Add("dispelling");
            //list.Add("rumination");
            //list.Add("ruminative");
            ////// page 68
            //list.Add("scrawl");
            //// page 69
            //list.Add("corral");
            //list.Add("slop");
            //// page 71
            //list.Add("preparatory");
            //// page 73
            //list.Add("impervious");
            //// page 78
            //list.Add("contemptuous");
            //// page 86
            //list.Add("circumscribe");
            //// page 91
            //list.Add("quandary");
            //// page 92
            //list.Add("elisions");
            //list.Add("elide");
            //// page 94
            //list.Add("perfunctory");
            //// page 97
            //list.Add("debilitating");
            //// page 100
            //list.Add("egregious");

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
