using System.ComponentModel.DataAnnotations;

namespace ConsoleHttpClientGeopunt30may2024
{
    public class GemeenteStraatAssociation
    {
        public GemeenteStraatAssociation()
        {

        }

        [Key]
        public int GemeenteStraatAssociationID { get; set; }

        public int GemeenteID { get; set; }

        public int StraatID { get; set; }
    }
}
