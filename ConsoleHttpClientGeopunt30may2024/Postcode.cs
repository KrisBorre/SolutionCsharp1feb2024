using System.ComponentModel.DataAnnotations;

namespace ConsoleHttpClientGeopunt30may2024
{
    public class Postcode
    {
        public Postcode()
        {

        }

        [Key]
        public int PostcodeID { get; set; }

        [Required]
        public string PostalCode { get; set; }

        public int GemeenteID { get; set; }
    }
}
