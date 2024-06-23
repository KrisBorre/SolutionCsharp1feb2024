using System.ComponentModel.DataAnnotations;

namespace ConsoleGeopunt30may2024
{
    public class Gemeente
    {
        public Gemeente()
        {

        }

        [Key]
        public int GemeenteID { get; set; }

        [Required]
        public string Naam { get; set; }
    }
}
