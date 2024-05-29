using System.ComponentModel.DataAnnotations;

namespace ConsoleHttpClientDictionary18may2024
{
    public class EnglishAntonym
    {
        [Key]
        public int AntonymID { get; set; }

        [Required]
        public int MeaningID { get; set; }

        [Required]
        public string Antonym { get; set; } = string.Empty;
    }
}
