using LibraryDatabaseDictionary25may2024;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDictionary30may2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DictionaryDbContext24may2024 dbContext = new DictionaryDbContext24may2024();

            List<string> list = new List<string>();

            Console.WriteLine("Discipline and Punish: The birth of the prison by Michel Foucault.");
            //// page 8
            //list.Add("discretion");
            //list.Add("pillory");
            //// page 9
            //list.Add("accustom");
            //list.Add("exemplary");
            //// page 10
            //list.Add("disavowal");
            //list.Add("expiation");
            //// page 11
            //list.Add("deprive");
            //// page 30
            //list.Add("decrepit");
            //list.Add("discourses");
            //// page 32
            //list.Add("reprimand");
            //list.Add("pecuniary");
            //// page 34
            //list.Add("effaced");
            //// page 35
            //list.Add("inscribed");
            //list.Add("denunciations");
            //// page 36
            //list.Add("writ");
            //// page 37
            //list.Add("casuistry");
            //// page 39
            //list.Add("perjury");
            // page 40
            list.Add("equivocal");
            // page 41
            list.Add("exculpation");
            // page 42
            list.Add("pursuance");
            // page 43
            list.Add("meted");
            // page 48
            list.Add("punctilious");
            // page 50
            list.Add("indissociably");
            // page 127
            list.Add("atonement");
            list.Add("expiation"); // see page 10
            // page 128

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
