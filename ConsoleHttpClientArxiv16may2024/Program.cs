using System.Xml.Serialization;

namespace ConsoleHttpClientArxiv16may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // arXiv.org hosts more than two million scholarly articles
            //https://info.arxiv.org/help/api/user-manual.html

            using var client = new HttpClient();

            //string word = "electron";
            //string word = "proton";
            string word = "neutron";

            string requestUri = "http://export.arxiv.org/api/query?search_query=all:" + word;

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string xmlString = await response.Content.ReadAsStringAsync();

            await Console.Out.WriteLineAsync(xmlString);

            // Create empty file in VS -> Paste Special -> Paste XML as class

            feed feed;

            using (StringReader reader = new StringReader(xmlString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(feed));
                feed = (feed)serializer.Deserialize(reader);
            }

            feedTitle feed_title = feed.title;

            feedEntry[] feed_entry = feed.entry;

            string value = feed_title.Value;
            await Console.Out.WriteLineAsync("value = " + value);

            int length = feed_entry.Length;
            await Console.Out.WriteLineAsync("length = " + length);

            var items = feed_entry[0].Items;

            await Console.Out.WriteLineAsync("items = " + items);

            /*
            value = ArXiv Query: search_query=all:electron&id_list=&start=0&max_results=10
            length = 10
            items = System.Object[]
            */

            /*
            value = ArXiv Query: search_query=all:proton&id_list=&start=0&max_results=10
            length = 10
            items = System.Object[]
            */

            /*
            value = ArXiv Query: search_query=all:neutron&id_list=&start=0&max_results=10
            length = 10
            items = System.Object[]
            */

            Console.Read();
        }
    }
}
