using LibraryDatabaseDictionary25may2024;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDictionary25may2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The structure of scientific revolutions by Thomas S. Kuhn.

            DictionaryDbContext24may2024 dbContext = new DictionaryDbContext24may2024();

            List<string> list = new List<string>();
            list.Add("adamant");
            list.Add("school of thought");
            list.Add("perspicuous");
            list.Add("moribund");
            list.Add("avocation");
            list.Add("avocational");
            list.Add("verisimilitude");
            list.Add("viability");
            list.Add("overt");
            list.Add("congeries"); // page 2
            list.Add("accidents"); // page 4
            list.Add("epistemology"); // page 8
            list.Add("esoteric"); // page 12
            list.Add("arduous"); // page 15
            list.Add("recondite"); // page 15
            list.Add("chaff"); // page 16
            list.Add("juxtapose"); // page 16
            list.Add("inchoate"); // page 17
            list.Add("unequivocal"); // page 21
            list.Add("preconception"); // page 39
            list.Add("penumbral"); // page 43
            list.Add("embroidery"); // page 47
            list.Add("ramshackle"); // page 49
            list.Add("askew"); // page 54
            list.Add("acidity"); // page 55
            list.Add("caloric"); // page 55
            list.Add("roasting"); // page 71
            list.Add("retention"); // page 72
            list.Add("concomitant"); // page 75
            list.Add("enunciation"); // page 75
            list.Add("tautologies"); // page 78
            list.Add("indictment"); // page 81
            list.Add("purported"); // page 102
            list.Add("inextricable"); // page 109
            list.Add("recourse"); // page 110           
            list.Add("concomitants"); // page 112
            list.Add("equivocal"); // page 116
            list.Add("inundates"); // page 122
            list.Add("incommensurability"); // page 147

            //string word1 = "electron";
            //list.Add(word1);

            foreach (string word in list)
            {
                var columnName1 = "Word";
                var columnValue1 = new SqliteParameter("columnValue", word);

                string sql4 = $"SELECT * FROM Words WHERE {columnName1} = @columnValue;";
                var list4 = dbContext.Words.FromSqlRaw(sql4, columnValue1).ToList();
                //Console.WriteLine(sql4);

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
