using LibraryArxiv24may2024;
using LibraryHttpClientArxiv24may2024;
using System.Xml.Serialization;

namespace ConsoleHttpClientArxiv27may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // arXiv.org hosts more than two million scholarly articles
            //https://info.arxiv.org/help/api/user-manual.html

            ArxivDbContext23may2024 dbContext = new ArxivDbContext23may2024();
            dbContext.Database.EnsureCreated();

            using var client = new HttpClient();

            // De reis van de mensheid, Johannes Kraus en Thomas Trappe (2022)
            List<string> list = new List<string>();
            list.Add("neandertal+genome");
            list.Add("archaic+hominin+group");
            list.Add("nuclear+dna+europe");
            list.Add("mitochondrial+dna+genome+hominin");
            list.Add("bonobo+genome+chimpanzee");
            list.Add("reduced+purine+biosynthesis");
            list.Add("human+brain+development");
            list.Add("human+germline+genome+editing");
            list.Add("evolution+modern+human+brain");
            list.Add("species+evolve+subdivided+populations");
            list.Add("human+effective+population+size+linkage+disequilibrium");
            list.Add("human+body+evolution+health+disease"); // pagina 291

            foreach (string word in list)
            {
                string requestUri = "http://export.arxiv.org/api/query?search_query=all:" + word;

                HttpResponseMessage response = await client.GetAsync(requestUri);

                response.EnsureSuccessStatusCode();

                string xmlString = await response.Content.ReadAsStringAsync();

                //Console.WriteLine(xmlString);

                using (StringReader reader = new StringReader(xmlString))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(feed));
                    feed feed = (feed)serializer.Deserialize(reader);

                    feedTitle feed_title = feed.title;

                    feedEntry[] feed_entry = feed.entry;

                    string value = feed_title.Value;
                    Console.WriteLine("title value = " + value);

                    int length = feed_entry.Length;
                    Console.WriteLine("length = " + length + " (number of publications)");

                    for (int i = 0; i < length; i++)
                    {
                        var entry = feed_entry[i];
                        object[] items = entry.Items;

                        Console.WriteLine("length = " + items.Length + " (number of items)");

                        bool alreadyExists = false;
                        Article article = new Article();

                        for (int j = 0; j < items.Length; j++)
                        {
                            object item = items[j];

                            if (item is feedEntryAuthor)
                            {
                                string name = ((feedEntryAuthor)item).name;
                                Console.WriteLine("Author name = " + name);
                                Contribution contribution = new Contribution();
                                contribution.Author = name;
                                article.Contributions.Add(contribution);
                            }
                            else if (item is feedEntryLink)
                            {
                                string href = ((feedEntryLink)item).href;
                                Console.WriteLine("Link href = " + href);
                                Link link = new Link();
                                link.Hyperlink = href;
                                article.Links.Add(link);
                            }
                            else if (item is feedEntryCategory)
                            {
                                string term = ((feedEntryCategory)item).term;
                                Console.WriteLine("term = " + term);
                            }
                            else if (item is primary_category)
                            {
                                string term = ((primary_category)item).term;
                                Console.WriteLine("primary category term = " + term);
                            }
                            else
                            {
                                Console.WriteLine("index = " + j + "     item = " + item.ToString());

                                if (j == 1)
                                {
                                    if (item is DateTime)
                                    {
                                        DateTime dateTime = (DateTime)item;
                                        article.DateTime1 = dateTime;
                                    }
                                }

                                if (j == 2)
                                {
                                    if (item is DateTime)
                                    {
                                        DateTime dateTime = (DateTime)item;
                                        article.DateTime2 = dateTime;
                                    }
                                }

                                if (j == 3)
                                {
                                    article.Title = item.ToString();

                                    foreach (Article existingArticle in dbContext.Articles.ToList())
                                    {
                                        if (existingArticle.Title == item.ToString())
                                        {
                                            alreadyExists = true;
                                            Console.WriteLine("The article with title '" + item.ToString() + "' already exists in the database!");
                                        }
                                    }
                                }

                                if (j == 4)
                                {
                                    article.Abstract = item.ToString();
                                }
                            }
                        }

                        if (!alreadyExists)
                        {
                            dbContext.Articles.Add(article);
                            dbContext.SaveChanges();
                        }

                        Console.WriteLine("\n-----------------------------------------------------------------------\n");
                    }
                }
            }

            Console.Read();
        }
    }
}
