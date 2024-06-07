using LibraryArxiv24may2024;
using LibraryHttpClientArxiv24may2024;
using System.Xml.Serialization;

namespace ConsoleHttpClientArxiv26may2024
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

            // Het neurologish complot, Greet Kayaert (2013)
            List<string> list = new List<string>();
            list.Add("nonconscious");
            list.Add("goal+pursuit");
            list.Add("synaptic+inhibition");
            list.Add("odor+discrimination");
            list.Add("facial+efference");
            list.Add("emotion");
            list.Add("ventral+visual+pathway");
            list.Add("human+visual+cortex");
            list.Add("cognition");
            list.Add("prefrontal+cortex");
            list.Add("cognitive+control");
            list.Add("automaticity+social+behavior");
            list.Add("dissociation+processes+belief");
            list.Add("emotional+tagging");
            list.Add("attention+intention");
            list.Add("abstract+thought");
            list.Add("color+information+object+recognition");
            list.Add("imitation+cognitive+neuroscience");
            list.Add("detection+salient+contours");
            list.Add("mathematical+cognition");
            list.Add("social+behaviors+elicited+electrical+stimulation");
            list.Add("conceptual+metaphors+affect");
            list.Add("somatosensory+processes+subserving+perception+action");
            list.Add("unsupervised+statistical+learning");
            list.Add("feedback+stimulus+distortion+acquisition+utilization");
            list.Add("semantic+priming+association+strength");
            list.Add("shape+tuning+macaque");
            list.Add("shape+dimensions+macaque");
            list.Add("closed+curve+incomplete+closure+figure");
            list.Add("position+dependent+visual+object+recognition");
            list.Add("caricature+effects+distinctiveness+identification");
            list.Add("counting+neurons+neurobiology+numerical+competence");
            list.Add("representation+number+brain");
            list.Add("role+context+object+recognition");
            list.Add("goal+implementation+benefits+costs+planning");


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
