using LibraryDatabaseDictionary25may2024;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDictionary29may2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DictionaryDbContext24may2024 dbContext = new DictionaryDbContext24may2024();

            List<string> list = new List<string>();

            Console.WriteLine("p53: The gene that cracked the cancer code by Sue Armstrong");
            //// page 9
            //list.Add("vivacious");
            //// page 10
            //list.Add("scuppered");
            //list.Add("scupper");
            //list.Add("poignantly");
            //list.Add("poignant");
            //// page 11
            //list.Add("froth");
            //// page 14
            //list.Add("perched");
            //list.Add("perch");
            //// page 17
            //list.Add("proliferative");
            //list.Add("proliferate");
            //// page 19
            //list.Add("proliferation");
            //// page 22
            //list.Add("metastases");
            //// page 29
            //list.Add("dispute");
            //list.Add("disputation");
            //// page 31
            //list.Add("dilatory");
            //// page 34
            //list.Add("fretted");
            //list.Add("fret");
            //// page 42
            //list.Add("gunge");
            //// page 70
            //list.Add("colon");
            //// page 101
            //list.Add("benign");
            //list.Add("malignant");
            //// page 103
            //list.Add("squirrelly");
            //// page 111
            //list.Add("carcinogen");
            //// page 112
            //list.Add("serendipity");
            //// page 129
            //list.Add("serendipity");
            ////page 162
            //list.Add("noxious");
            //// page 172
            //list.Add("coincidence");
            //// page 179
            //list.Add("cervix");
            // page 206


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
