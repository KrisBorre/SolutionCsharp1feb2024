namespace ConsoleHttpClientDisney18may2024
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        public int _id { get; set; }
        public List<string> films { get; set; }
        public List<string> shortFilms { get; set; }
        public List<string> tvShows { get; set; }
        public List<string> videoGames { get; set; }
        public List<string> parkAttractions { get; set; }
        public List<object> allies { get; set; }
        public List<object> enemies { get; set; }
        public string sourceUrl { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string url { get; set; }
        public int __v { get; set; }
    }

    public class Info
    {
        public int count { get; set; }
        public int totalPages { get; set; }
        public object previousPage { get; set; }
        public object nextPage { get; set; }
    }

    public class Root
    {
        public Info info { get; set; }
        public List<Datum> data { get; set; }
    }

}
