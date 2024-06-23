using Microsoft.EntityFrameworkCore;

namespace ConsoleGeopunt30may2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallo Geopunt databank!");

            GeopuntDbContext30may2024 dbContext = new GeopuntDbContext30may2024();

            dbContext.Database.EnsureCreated();

            List<Adres> allAdresses = dbContext.Adressen
                .Include(a => a.Postcode)
                .Include(a => a.Gemeente)
                .Include(a => a.Straat)
                .Include(a => a.Huisnummer)
                .ToList();


            List<StraatHuisnummerAssociation> straatHuisnummerAssociations = dbContext.StraatHuisnummerAssociations.ToList();

            string houseNumber = "137";
            Console.WriteLine("huisnummer: " + houseNumber);
            List<Huisnummer> huisnummers = dbContext.Huisnummers.Where(h => h.HouseNumber == houseNumber).ToList();

            List<int> straatIDs = new List<int>();
            foreach (var nummer in huisnummers)
            {
                List<StraatHuisnummerAssociation> associations = dbContext.StraatHuisnummerAssociations.Where(sha => sha.HuisnummerID == nummer.HuisnummerID).ToList();
                foreach (var association in associations)
                {
                    straatIDs.Add(association.StraatID);
                }
            }

            List<Straat> straten = dbContext.Straten.Where(s => straatIDs.Contains(s.StraatID)).ToList();

            Console.WriteLine("Straten: ");
            foreach (var straat in straten)
            {
                Console.WriteLine(straat.Naam);
            }
            Console.WriteLine();

            List<GemeenteStraatAssociation> gemeenteStraatAssociations = dbContext.GemeenteStraatAssociations.Where(gsa => straatIDs.Contains(gsa.StraatID)).ToList();

            List<int> gemeenteIds = new List<int>();
            foreach (GemeenteStraatAssociation gsa in gemeenteStraatAssociations)
            {
                gemeenteIds.Add(gsa.GemeenteID);
            }

            List<Gemeente> gemeentes = dbContext.Gemeentes.Where(g => gemeenteIds.Contains(g.GemeenteID)).ToList();

            Console.WriteLine("Gemeentes: ");
            foreach (var gemeente in gemeentes)
            {
                Console.WriteLine(gemeente.Naam);
            }

            Console.ReadLine();
        }
    }
}
