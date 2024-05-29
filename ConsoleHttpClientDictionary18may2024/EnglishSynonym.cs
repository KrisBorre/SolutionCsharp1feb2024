using System.ComponentModel.DataAnnotations;

namespace ConsoleHttpClientDictionary18may2024
{
    public class EnglishSynonym
    {
        [Key]
        public int SynonymID { get; set; }

        [Required]
        public int MeaningID { get; set; }

        [Required]
        public string Synonym { get; set; } = string.Empty;
    }
}
