using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ConsoleHttpClientDictionary24may2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Dictionary database!");

            DictionaryDbContext24may2024 dbContext = new DictionaryDbContext24may2024();

            List<EnglishWord> words = dbContext.Words.ToList();
            bool alreadyExists = false;
            foreach (EnglishWord englishWord in words)
            {
                Console.WriteLine(englishWord.Word);
            }

            Console.WriteLine();

            FormattableString sql1 = $"SELECT * FROM Words;";
            var list1 = dbContext.Words.FromSql(sql1).ToList();
            Console.WriteLine(sql1);
            foreach (EnglishWord englishWord in list1)
            {
                Console.WriteLine(englishWord.Word);
            }

            Console.WriteLine();

            FormattableString sql2 = $"SELECT * FROM Words WHERE Word = 'hello';";
            var list2 = dbContext.Words.FromSql(sql2).ToList();
            Console.WriteLine(sql2);
            foreach (EnglishWord englishWord in list2)
            {
                Console.WriteLine(englishWord.Word);
            }

            Console.WriteLine();

            FormattableString sql3 = $"SELECT * FROM Words WHERE Word = 'Hello';";
            var list3 = dbContext.Words.FromSql(sql3).ToList();
            Console.WriteLine(sql3);
            foreach (EnglishWord englishWord in list3)
            {
                Console.WriteLine("Word: " + englishWord.Word);
            }

            Console.WriteLine();


            FormattableString sql4 = $"SELECT * FROM Words WHERE Word = 'electron';";
            var list4 = dbContext.Words.FromSql(sql4).ToList();
            Console.WriteLine(sql4);
            if (list4.Count == 1)
            {
                EnglishWord englishWord = list4[0];
                Console.WriteLine("Word: " + englishWord.Word);
                int wordId = englishWord.WordID;

                var columnName1 = "WordId";
                var columnValue1 = new SqliteParameter("columnValue", wordId);

                string sql5 = $"SELECT * FROM Meanings WHERE {columnName1} = @columnValue";
                List<EnglishMeaning> list5 = dbContext.Meanings.FromSqlRaw(sql5, columnValue1).ToList();
                foreach (EnglishMeaning meaning in list5)
                {
                    Console.WriteLine("Part of speech: " + meaning.PartOfSpeech);
                    englishWord.Meanings.Add(meaning);

                    string sql6 = $"SELECT * FROM Definitions WHERE MeaningID = (SELECT MeaningID FROM Meanings WHERE {columnName1} = @columnValue);";
                    List<EnglishDefinition> list6 = dbContext.Definitions.FromSqlRaw(sql6, columnValue1).ToList();
                    foreach (EnglishDefinition definition in list6)
                    {
                        if (meaning.MeaningID == definition.MeaningID)
                        {
                            meaning.Definitions.Add(definition);
                            Console.WriteLine("Definition: " + definition.Definition);
                            Console.WriteLine("Example: " + definition.Example);
                        }
                    }

                    string sql7 = $"SELECT * FROM Synonyms WHERE MeaningID = (SELECT MeaningID FROM Meanings WHERE {columnName1} = @columnValue);";
                    List<EnglishSynonym> list7 = dbContext.Synonyms.FromSqlRaw(sql7, columnValue1).ToList();
                    foreach (EnglishSynonym synonym in list7)
                    {
                        if (meaning.MeaningID == synonym.MeaningID)
                        {
                            meaning.Synonyms.Add(synonym);
                            Console.WriteLine("Synonym: " + synonym.Synonym);
                        }
                    }

                    string sql8 = $"SELECT * FROM Antonyms WHERE MeaningID = (SELECT MeaningID FROM Meanings WHERE {columnName1} = @columnValue);";
                    List<EnglishAntonym> list8 = dbContext.Antonyms.FromSqlRaw(sql8, columnValue1).ToList();
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

            Console.WriteLine();


            Console.Read();
        }
    }
}
