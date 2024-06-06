using LibraryDatabaseDictionary25may2024;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConsoleDictionary25may2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The unwritten rules of PhD research by M. Petre and G. Rugg

            DictionaryDbContext24may2024 dbContext = new DictionaryDbContext24may2024();

            List<string> list = new List<string>();
            list.Add("scant");
            list.Add("tacit");
            list.Add("apocryphal");
            list.Add("fiddly");
            list.Add("insidious");
            list.Add("fiendish"); // page 1
            list.Add("gloom"); // page 1
            list.Add("adject"); // page 2
            list.Add("drudgery"); // page 2
            list.Add("disparaging"); // page 2
            list.Add("discourse"); // page 2
            list.Add("sordidly"); // page 3
            list.Add("digression"); // page 4
            list.Add("digress"); // page 4
            list.Add("erudition"); // page 4
            list.Add("erudite"); // page 4
            list.Add("scrutinized"); // page 6
            list.Add("scrutiny"); // page 6
            list.Add("cognizance"); // page 7
            list.Add("strands"); // page 7
            list.Add("strand"); // page 7
            list.Add("anthology"); // page 8
            list.Add("woefully"); // page 9
            list.Add("woeful"); // page 9
            list.Add("stroppy"); // page 10
            list.Add("substantiation"); // page 12
            list.Add("taxonomy"); // page 15
            list.Add("seminal"); // page 16
            list.Add("stocks"); // page 17
            list.Add("probation"); // page 19
            list.Add("taught"); // page 22
            list.Add("teach"); // page 22
            list.Add("scrounge"); // page 28
            list.Add("inviolable"); // page 30
            list.Add("inoculation"); // page 35
            list.Add("inoculate"); // page 35
            list.Add("couch"); // page 37
            list.Add("elicitation"); // page 39
            list.Add("elicit"); // page 39
            list.Add("fastidious"); // page 39
            list.Add("collation"); // page 41
            list.Add("pathological"); // page 44
            list.Add("remunerated"); // page 46
            list.Add("renumerate"); // page 46
            list.Add("liability"); // page 48
            list.Add("beleaguers"); // page 49
            list.Add("beleaguer"); // page 49
            list.Add("sulking"); // page 51
            list.Add("sulky"); // page 51
            list.Add("forestall"); // page 52
            list.Add("pesky"); // page 53
            list.Add("incisive"); // page 59
            list.Add("incise"); // page 59
            list.Add("dissonance"); // page 61
            list.Add("dissonant"); // page 61
            list.Add("incentive"); // page 61
            list.Add("inundated"); // page 62
            list.Add("inundate"); // page 62
            list.Add("trawls"); // page 72
            list.Add("trawl"); // page 72
            list.Add("sufferance"); // page 73
            list.Add("fastidious"); // page 81
            list.Add("fastidiously"); // page 81
            list.Add("inflammatory"); // page 83
            list.Add("seminal"); // page 83
            list.Add("voraciously"); // page 85
            list.Add("voracious"); // page 85
            list.Add("hare"); // page 85
            list.Add("astute"); // page 87
            list.Add("kudos"); // page 90
            list.Add("antecedents"); // page 140
            list.Add("antecedent"); // page 140
            list.Add("tacitly"); // page 146
            list.Add("tacit"); // page 146
            list.Add("gnarled"); // page 165
            list.Add("gnarl"); // page 165
            list.Add("baleful"); // page 245
            list.Add("amenability"); // page 248
            list.Add("amenable"); // page 248
            list.Add("austere"); // page 256
   

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
