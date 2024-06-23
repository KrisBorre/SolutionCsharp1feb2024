using System.ComponentModel.DataAnnotations;

namespace ConsoleHttpClientGeopunt30may2024
{
    public class Huisnummer
    {
        public Huisnummer()
        {

        }

        [Key]
        public int HuisnummerID { get; set; }

        public string HouseNumber { get; set; }
    }
}
