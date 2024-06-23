using System.ComponentModel.DataAnnotations;


namespace ConsoleGeopunt30may2024
{
    public class Straat
    {
        public Straat()
        {

        }

        [Key]
        public int StraatID { get; set; }

        [Required]
        public string Naam { get; set; }
    }
}
