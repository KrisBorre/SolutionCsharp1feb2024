using System.ComponentModel.DataAnnotations;

namespace ConsoleHttpClientGeopunt30may2024
{
    public class Adres
    {
        public Adres()
        {

        }

        [Key]
        public int AdresID { get; set; }

        public int PostcodeID { get; set; }

        public Postcode Postcode { get; set; }

        public int GemeenteID { get; set; }

        public Gemeente Gemeente { get; set; }

        public int StraatID { get; set; }

        public Straat Straat { get; set; }

        public int HuisnummerID { get; set; }

        public Huisnummer Huisnummer { get; set; }

    }
}
