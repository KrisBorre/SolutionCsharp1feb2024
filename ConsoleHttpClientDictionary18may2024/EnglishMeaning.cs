using System.ComponentModel.DataAnnotations;

namespace ConsoleHttpClientDictionary18may2024
{
    public class EnglishMeaning
    {
        public EnglishMeaning()
        {
            Definitions = new List<EnglishDefinition>();
            Synonyms = new List<EnglishSynonym>();
            Antonyms = new List<EnglishAntonym>();
        }

        [Key]
        public int MeaningID { get; set; }

        [Required]
        public int WordID { get; set; }

        public string? PartOfSpeech { get; set; } = string.Empty;

        public List<EnglishDefinition> Definitions { get; set; }

        public List<EnglishSynonym> Synonyms { get; set; }
        public List<EnglishAntonym> Antonyms { get; set; }
    }
}
