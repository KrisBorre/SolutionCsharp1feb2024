using Newtonsoft.Json;

namespace ConsoleHttpClientGeopunt30may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // https://www.geopunt.be/

            GeopuntDbContext30may2024 dbContext = new GeopuntDbContext30may2024();
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            List<string> list = new List<string>();

            //list.Add("Kasteeldreef 137, 2970 Schilde");
            //list.Add("Somersstraat 22, Antwerpen");
            //list.Add("Kerkelei 57, Schilde");
            //list.Add("Groenendaallaan 394, Antwerpen");
            //list.Add("Vordensteinstraat 76, Schoten");
            //list.Add("Van Immerseelstraat 2, Antwerpen");
            //list.Add("Koningin Astridlaan 50, 3500 Hasselt");
            //list.Add("Spinnerslaan 12, 9160 Lokeren");
            //list.Add("Poortakkerstraat 91, 9051 Gent");
            //list.Add("Kortrijksesteenweg 1005, 9000 Gent");
            //list.Add("Steenweg op Antwerpen 26, 2300 Turnhout");
            //list.Add("Stationsstraat 13, 3060 Korbeek-Dijle");
            //list.Add("Ringlaan 17A, 2960 Brecht"); // Niet gevonden

            //list.Add("Imperiastraat 10, 1930 Zaventem");
            //list.Add("Ieperseweg 87, 8800 Beitem");
            //list.Add("Koning Leopold III laan, 8200 Sint-Andries");
            //list.Add("Provincieplein 1, 3010 Leuven");
            //list.Add("Fruittuinweg 1, 3800 SINT–TRUIDEN");
            //list.Add("Burgemeester van Gansberghelaan 92, 9820 Merelbeke"); // Niet gevonden

            using var client = new HttpClient();

            foreach (string query in list)
            {
                string BaseUrl = "https://geo.api.vlaanderen.be/geolocation/v4/suggestion";
                string QueryParamName = "q";
                var url = $"{BaseUrl}?{QueryParamName}={query}";
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseString);

                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseString);

                    Console.WriteLine("myDeserializedClass.SuggestionResult = ");
                    foreach (string suggestion in myDeserializedClass.SuggestionResult)
                    {
                        Console.WriteLine(suggestion);

                        // string query = "Kasteeldreef 137, 2970 Schilde";
                        // "{\"SuggestionResult\":[\"Kasteeldreef 137, 2970 Schilde\"]}"

                        Adres adres = ExtractAddress(suggestion);

                        if (adres != null)
                        {
                            bool postcodeAlreadyExists = false;
                            Postcode? postCode = dbContext.Postcodes.SingleOrDefault(pc => pc.PostalCode == adres.Postcode.PostalCode);
                            if (postCode != null)
                            {
                                postcodeAlreadyExists = true;
                                adres.PostcodeID = postCode.PostcodeID;
                            }
                            else
                            {
                                postcodeAlreadyExists = false;
                                dbContext.Postcodes.Add(adres.Postcode);
                                dbContext.SaveChanges();
                                adres.PostcodeID = adres.Postcode.PostcodeID;
                            }

                            bool gemeenteAlreadyExists = false;
                            Gemeente? gemeente = dbContext.Gemeentes.SingleOrDefault(g => g.Naam == adres.Gemeente.Naam);
                            if (gemeente != null)
                            {
                                gemeenteAlreadyExists = true;
                                adres.GemeenteID = gemeente.GemeenteID;
                                if (!postcodeAlreadyExists) adres.Postcode.GemeenteID = gemeente.GemeenteID;
                            }
                            else
                            {
                                gemeenteAlreadyExists = false;
                                dbContext.Gemeentes.Add(adres.Gemeente);
                                dbContext.SaveChanges();
                                adres.GemeenteID = adres.Gemeente.GemeenteID;
                                if (!postcodeAlreadyExists) adres.Postcode.GemeenteID = adres.Gemeente.GemeenteID;
                            }

                            bool straatAlreadyExists = false;
                            Straat? straat = dbContext.Straten.SingleOrDefault(s => s.Naam == adres.Straat.Naam);
                            if (straat != null)
                            {
                                straatAlreadyExists = true;
                                adres.StraatID = straat.StraatID;
                            }
                            else
                            {
                                straatAlreadyExists = false;
                                dbContext.Straten.Add(adres.Straat);
                                dbContext.SaveChanges();
                                adres.StraatID = adres.Straat.StraatID;
                            }

                            bool huisnummerAlreadyExists = false;
                            Huisnummer? huisnummer = dbContext.Huisnummers.SingleOrDefault(h => h.HouseNumber == adres.Huisnummer.HouseNumber);
                            if (huisnummer != null)
                            {
                                huisnummerAlreadyExists = true;
                                adres.HuisnummerID = huisnummer.HuisnummerID;
                            }
                            else
                            {
                                huisnummerAlreadyExists = false;
                                dbContext.Huisnummers.Add(adres.Huisnummer);
                                dbContext.SaveChanges();
                                adres.HuisnummerID = adres.Huisnummer.HuisnummerID;
                                adres.Gemeente = null;
                                adres.Postcode = null;
                                adres.Straat = null;
                                adres.Huisnummer = null;
                                dbContext.Adressen.Add(adres);
                            }

                            GemeenteStraatAssociation? gsa = dbContext.GemeenteStraatAssociations.SingleOrDefault(gsa => (gsa.GemeenteID == adres.GemeenteID && gsa.StraatID == adres.StraatID));
                            if (gsa == null)
                            {
                                GemeenteStraatAssociation gemeenteStraatAssociation = new GemeenteStraatAssociation();
                                gemeenteStraatAssociation.GemeenteID = adres.GemeenteID;
                                gemeenteStraatAssociation.StraatID = adres.StraatID;
                                dbContext.GemeenteStraatAssociations.Add(gemeenteStraatAssociation);
                            }

                            StraatHuisnummerAssociation? sha = dbContext.StraatHuisnummerAssociations.SingleOrDefault(sha => (sha.StraatID == adres.StraatID && sha.HuisnummerID == adres.HuisnummerID));
                            if (sha == null)
                            {
                                StraatHuisnummerAssociation straatHuisnummerAssociation = new StraatHuisnummerAssociation();
                                straatHuisnummerAssociation.StraatID = adres.StraatID;
                                straatHuisnummerAssociation.HuisnummerID = adres.HuisnummerID;
                                dbContext.StraatHuisnummerAssociations.Add(straatHuisnummerAssociation);
                            }

                            dbContext.SaveChanges();
                        }
                    }

                    // string query = "Veld";
                    // "{\"SuggestionResult\":[\"Assenede\",\"Evergem\",\"Lanaken\",\"Lichtervelde\",\"Lochristi\"]}"

                    /*
                    Assenede
                    Evergem
                    Lanaken
                    Lichtervelde
                    Lochristi
                    */
                }
            }

            Console.ReadLine();
        }

        public static Adres ExtractAddress(string addressString)
        {
            Adres adres = new Adres();

            var splitAddress = addressString.Split(',');

            if (splitAddress.Length < 2)
            {
                Console.WriteLine("Invalid address format. ");
                return null;
            }

            string streetAndHousenumber = splitAddress[0].Trim();

            string[] streetAndNumber = splitAddress[0].Trim().Split(' ');

            if (streetAndNumber.Length < 2)
            {
                Console.WriteLine("Invalid address format. ");
                return null;
            }

            string streetName = streetAndNumber[0];
            for (int i = 1; i < streetAndNumber.Length - 1; i++)
            {
                streetName += " " + streetAndNumber[i];
            }
            string houseNumber = streetAndNumber[streetAndNumber.Length - 1];

            string[] postalCodeAndMunicipality = splitAddress[1].Trim().Split(' ');

            string postalCode = postalCodeAndMunicipality[0];
            string municipality = postalCodeAndMunicipality[1];

            Console.WriteLine($"Street: {streetName}");
            Straat straat = new Straat();
            straat.Naam = streetName;
            Console.WriteLine($"House Number: {houseNumber}");
            Huisnummer huisnummer = new Huisnummer();
            huisnummer.HouseNumber = houseNumber;
            Console.WriteLine($"Postal Code: {postalCode}");
            Postcode postcode = new Postcode();
            postcode.PostalCode = postalCode;
            Console.WriteLine($"Municipality: {municipality}");
            Gemeente gemeente = new Gemeente();
            gemeente.Naam = municipality;

            adres.Postcode = postcode;
            adres.Gemeente = gemeente;
            adres.Straat = straat;
            adres.Huisnummer = huisnummer;

            return adres;
        }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Root
    {
        public List<string> SuggestionResult { get; set; }
    }
}
