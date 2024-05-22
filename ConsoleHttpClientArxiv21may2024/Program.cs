using System.Xml.Serialization;

namespace ConsoleHttpClientArxiv21may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // arXiv.org hosts more than two million scholarly articles
            //https://info.arxiv.org/help/api/user-manual.html

            using var client = new HttpClient();

            string word = "neutron";

            string requestUri = "http://export.arxiv.org/api/query?search_query=all:" + word;

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string xmlString = await response.Content.ReadAsStringAsync();

            await Console.Out.WriteLineAsync(xmlString);

            XmlSerializer serializer = new XmlSerializer(typeof(Feed));
            using (StringReader reader = new StringReader(xmlString))
            {
                Feed feed = (Feed)serializer.Deserialize(reader); // Exception 'There is an error in XML document (2, 2).'

                Entry entry = feed.Entry;
                Title title = entry.Title;
                string title_text = title.Text;
                Summary summary = entry.Summary;
                string summary_text = summary.Text;
            }

            Console.ReadLine();
        }
    }
}
