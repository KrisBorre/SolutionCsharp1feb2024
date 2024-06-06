using System.ComponentModel.DataAnnotations;

namespace LibraryDatabaseDictionary25may2024
{
    public class EnglishWord
    {
        public EnglishWord()
        {
            Meanings = new List<EnglishMeaning>();
        }

        [Key]
        public int WordID { get; set; }

        [Required]
        public string Word { get; set; } = string.Empty;

        public List<EnglishMeaning> Meanings { get; set; }
    }
}
