using System.ComponentModel.DataAnnotations;

namespace ConsoleHttpClientGeopunt30may2024
{
    public class StraatHuisnummerAssociation
    {
        public StraatHuisnummerAssociation()
        {

        }

        [Key]
        public int StraatHuisnummerAssociationID { get; set; }

        public int StraatID { get; set; }

        public int HuisnummerID { get; set; }
    }
}
