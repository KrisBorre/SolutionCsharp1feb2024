using System.ComponentModel.DataAnnotations;

namespace ConsoleHttpClientDictionary24may2024
{
    public class EnglishDefinition
    {
        [Key]
        public int DefinitionID { get; set; }

        [Required]
        public int MeaningID { get; set; }

        [Required]
        public string Definition { get; set; } = string.Empty;

        public string? Example { get; set; } = string.Empty;
    }
}
