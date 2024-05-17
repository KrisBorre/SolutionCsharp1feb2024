namespace ConsoleHttpClientArchive16may2024
{
    public class Rootobject
    {
        public Responseheader responseHeader { get; set; }
        public Response response { get; set; }
    }

    public class Responseheader
    {
        public int status { get; set; }
        public int QTime { get; set; }
        public Params _params { get; set; }
    }

    public class Params
    {
        public string query { get; set; }
        public string qin { get; set; }
        public string fields { get; set; }
        public string wt { get; set; }
        public string rows { get; set; }
        public int start { get; set; }
    }

    public class Response
    {
        public int numFound { get; set; }
        public int start { get; set; }
        public Doc[] docs { get; set; }
    }

    public class Doc
    {
        public string backup_location { get; set; }
        public string btih { get; set; }
        public string[] collection { get; set; }
        public string creator { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public int downloads { get; set; }
        public string[] format { get; set; }
        public string identifier { get; set; }
        public string[] indexflag { get; set; }
        public long item_size { get; set; }
        public string mediatype { get; set; }
        public int month { get; set; }
        public DateTime[] oai_updatedate { get; set; }
        public DateTime publicdate { get; set; }
        public string publisher { get; set; }
        public string rights { get; set; }
        public object stripped_tags { get; set; }
        public string[] subject { get; set; }
        public string title { get; set; }
        public int week { get; set; }
        public int year { get; set; }
        public string language { get; set; }
    }
}