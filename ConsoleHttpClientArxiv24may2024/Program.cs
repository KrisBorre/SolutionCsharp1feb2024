using LibraryArxiv24may2024;
using LibraryHttpClientArxiv24may2024;
using System.Xml.Serialization;

namespace ConsoleHttpClientArxiv24may2024
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // arXiv.org hosts more than two million scholarly articles
            //https://info.arxiv.org/help/api/user-manual.html

            ArxivDbContext23may2024 dbContext = new ArxivDbContext23may2024();
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            using var client = new HttpClient();

            // string word = "adiabatic";
            // string word = "invariant";
            // string word = "adiabatic+invariant";
            // string word = "pendulum";
            // string word = "elliptic+functions";
            // string word = "p53";
            // string word = "genetics";
            // string word = "neuroscience";
            // string word = "evolution";
            // string word = "gene+algorithms";
            // string word = "nonlinear+dynamics";
            // string word = "brain+p53";
            string word = "electron";
            string word2 = "proton";
            string word3 = "neutron";

            string requestUri = "http://export.arxiv.org/api/query?search_query=all:" + word;

            HttpResponseMessage response = await client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();

            string xmlString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(xmlString);

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

            /*title value = ArXiv Query: search_query=all:neuroscience&id_list=&start=0&max_results=10
            length = 10 (number of publications)
            length = 13 (number of items)
            index = 0     item = http://arxiv.org/abs/1412.5909v2
            index = 1     item = 16/03/2015 17:09:21
            index = 2     item = 18/12/2014 15:52:08
            index = 3     item = Teaching Computational Neuroscience
            index = 4     item =   The problems and beauty of teaching computational neuroscience are discussed
            by reviewing three new textbooks.

            Author name = Péter Érdi
            index = 6     item = 10.1007/s11571-015-9340-6.
            Link href = http://dx.doi.org/10.1007/s11571-015-9340-6.
            index = 8     item = 8 pages
            Link href = http://arxiv.org/abs/1412.5909v2
            Link href = http://arxiv.org/pdf/1412.5909v2
            primary category term = q-bio.NC
            term = q-bio.NC

            -----------------------------------------------------------------------

            length = 14 (number of items)
            index = 0     item = http://arxiv.org/abs/2212.04195v2
            index = 1     item = 4/03/2023 0:51:41
            index = 2     item = 8/12/2022 11:23:07
            index = 3     item = A Paradigm Shift in Neuroscience Driven by Big Data: State of art,
              Challenges, and Proof of Concept
            index = 4     item =   A recent editorial in Nature noted that cognitive neuroscience is at a
            crossroads where it is a thorny issue to reliably reveal brain-behavior
            associations. This commentary sketches a big data science way out for cognitive
            neuroscience, namely population neuroscience. In terms of design, analysis, and
            interpretations, population neuroscience research takes the design control to
            an unprecedented level, greatly expands the dimensions of the data analysis
            space, and paves a paradigm shift for exploring mechanisms on brain-behavior
            associations.

            Author name = Zi-Xuan Zhou
            Author name = Xi-Nian Zuo
            index = 7     item = 6 pages, 1 figure
            Link href = http://arxiv.org/abs/2212.04195v2
            Link href = http://arxiv.org/pdf/2212.04195v2
            primary category term = q-bio.NC
            term = q-bio.NC
            term = q-bio.QM
            term = stat.ME

            -----------------------------------------------------------------------

            length = 12 (number of items)
            index = 0     item = http://arxiv.org/abs/1605.01905v1
            index = 1     item = 6/05/2016 12:06:15
            index = 2     item = 6/05/2016 12:06:15
            index = 3     item = What can topology tell us about the neural code?
            index = 4     item =   Neuroscience is undergoing a period of rapid experimental progress and
            expansion. New mathematical tools, previously unknown in the neuroscience
            community, are now being used to tackle fundamental questions and analyze
            emerging data sets. Consistent with this trend, the last decade has seen an
            uptick in the use of topological ideas and methods in neuroscience. In this
            talk I will survey recent applications of topology in neuroscience, and explain
            why topology is an especially natural tool for understanding neural codes.
            Note: This is a write-up of my talk for the Current Events Bulletin, held at
            the 2016 Joint Math Meetings in Seattle, WA.

            Author name = Carina Curto
            index = 6     item = 16 pages, 9 figures
            index = 7     item = Bulletin of the AMS, vol. 54, no. 1, pp. 63-78, January 2017
            Link href = http://arxiv.org/abs/1605.01905v1
            Link href = http://arxiv.org/pdf/1605.01905v1
            primary category term = q-bio.NC
            term = q-bio.NC

            -----------------------------------------------------------------------

            length = 12 (number of items)
            index = 0     item = http://arxiv.org/abs/1701.01219v1
            index = 1     item = 5/01/2017 6:07:48
            index = 2     item = 5/01/2017 6:07:48
            index = 3     item = Is neuroscience facing up to statistical power?
            index = 4     item =   It has been demonstrated that the statistical power of many neuroscience
            studies is very low, so that the results are unlikely to be robustly
            reproducible. How are neuroscientists and the journals in which they publish
            responding to this problem? Here I review the sample size justifications
            provided for all 15 papers published in one recent issue of the leading journal
            Nature Neuroscience. Of these, only one claimed it was adequately powered. The
            others mostly appealed to the sample sizes used in earlier studies, despite a
            lack of evidence that these earlier studies were adequately powered. Thus,
            concerns regarding statistical power in neuroscience have mostly not yet been
            addressed.

            Author name = Geoffrey J Goodhill
            index = 6     item = 5 pages
            Link href = http://arxiv.org/abs/1701.01219v1
            Link href = http://arxiv.org/pdf/1701.01219v1
            primary category term = q-bio.NC
            term = q-bio.NC
            term = stat.AP

            -----------------------------------------------------------------------

            length = 15 (number of items)
            index = 0     item = http://arxiv.org/abs/1805.08239v2
            index = 1     item = 26/11/2018 15:36:21
            index = 2     item = 21/05/2018 18:11:26
            index = 3     item = The Roles of Supervised Machine Learning in Systems Neuroscience
            index = 4     item =   Over the last several years, the use of machine learning (ML) in neuroscience
            has been rapidly increasing. Here, we review ML's contributions, both realized
            and potential, across several areas of systems neuroscience. We describe four
            primary roles of ML within neuroscience: 1) creating solutions to engineering
            problems, 2) identifying predictive variables, 3) setting benchmarks for simple
            models of the brain, and 4) serving itself as a model for the brain. The
            breadth and ease of its applicability suggests that machine learning should be
            in the toolbox of most systems neuroscientists.

            Author name = Joshua I. Glaser
            Author name = Ari S. Benjamin
            Author name = Roozbeh Farhoodi
            Author name = Konrad P. Kording
            Link href = http://arxiv.org/abs/1805.08239v2
            Link href = http://arxiv.org/pdf/1805.08239v2
            primary category term = q-bio.NC
            term = q-bio.NC
            term = cs.LG
            term = stat.ML

            -----------------------------------------------------------------------

            length = 12 (number of items)
            index = 0     item = http://arxiv.org/abs/2305.06037v2
            index = 1     item = 29/05/2023 16:06:05
            index = 2     item = 10/05/2023 10:42:23
            index = 3     item = Connecting levels of analysis in the computational era
            index = 4     item =   Neuroscience and artificial intelligence are closely intertwined, but so are
            the physics of dynamical system, philosophy and psychology. Each of these
            fields try in their own way to relate observations at the level of molecules,
            synapses, neurons or behavior, to a function. An influential conceptual
            approach to this end was popularized by David Marr, which focused on the
            interaction between three theoretical 'levels of analysis'. With the
            convergence of simulation-based approaches, algorithm-oriented Neuro-AI and
            high-throughput data, we currently see much research organized around four
            levels of analysis: observations, models, algorithms and functions.
            Bidirectional interaction between these levels influences how we undertake
            interdisciplinary science.

            Author name = Richard Naud
            Author name = André Longtin
            index = 7     item = neuroscience, cognitive neuroscience, computational neuroscience,
              machine learning, artificial intelligence, philosophy
            Link href = http://arxiv.org/abs/2305.06037v2
            Link href = http://arxiv.org/pdf/2305.06037v2
            primary category term = q-bio.NC
            term = q-bio.NC

            -----------------------------------------------------------------------

            length = 12 (number of items)
            index = 0     item = http://arxiv.org/abs/2403.15413v1
            index = 1     item = 6/03/2024 12:38:18
            index = 2     item = 6/03/2024 12:38:18
            index = 3     item = Playing With Neuroscience: Past, Present and Future of Neuroimaging and
              Games
            index = 4     item =   Videogames have been a catalyst for advances in many research fields, such as
            artificial intelligence, human-computer interaction or virtual reality. Over
            the years, research in fields such as artificial intelligence has enabled the
            design of new types of games, while games have often served as a powerful tool
            for testing and simulation. Can this also happen with neuroscience? What is the
            current relationship between neuroscience and games research? what can we
            expect from the future? In this article, we'll try to answer these questions,
            analysing the current state-of-the-art at the crossroads between neuroscience
            and games and envisioning future directions.

            Author name = Paolo Burelli
            Author name = Laurits Dixen
            Link href = http://arxiv.org/abs/2403.15413v1
            Link href = http://arxiv.org/pdf/2403.15413v1
            primary category term = q-bio.NC
            term = q-bio.NC
            term = cs.AI

            -----------------------------------------------------------------------

            length = 12 (number of items)
            index = 0     item = http://arxiv.org/abs/1403.5701v1
            index = 1     item = 22/03/2014 20:30:55
            index = 2     item = 22/03/2014 20:30:55
            index = 3     item = Cortex simulation system proposal using distributed computer network
              environments
            index = 4     item =   In the dawn of computer science and the eve of neuroscience we participate in
            rebirth of neuroscience due to new technology that allows us to deeply and
            precisely explore whole new world that dwells in our brains.

            Author name = Boris Tomas
            index = 6     item = 4 pages
            index = 7     item = IJCSIS Volume 12 No. 3 2014
            Link href = http://arxiv.org/abs/1403.5701v1
            Link href = http://arxiv.org/pdf/1403.5701v1
            primary category term = cs.AI
            term = cs.AI

            -----------------------------------------------------------------------

            length = 15 (number of items)
            index = 0     item = http://arxiv.org/abs/2301.05057v1
            index = 1     item = 19/12/2022 9:09:40
            index = 2     item = 19/12/2022 9:09:40
            index = 3     item = An overview of open source Deep Learning-based libraries for
              Neuroscience
            index = 4     item =   In recent years, deep learning revolutionized machine learning and its
            applications, producing results comparable to human experts in several domains,
            including neuroscience. Each year, hundreds of scientific publications present
            applications of deep neural networks for biomedical data analysis. Due to the
            fast growth of the domain, it could be a complicated and extremely
            time-consuming task for worldwide researchers to have a clear perspective of
            the most recent and advanced software libraries. This work contributes to
            clarify the current situation in the domain, outlining the most useful
            libraries that implement and facilitate deep learning application to
            neuroscience, allowing scientists to identify the most suitable options for
            their research or clinical projects. This paper summarizes the main
            developments in Deep Learning and their relevance to Neuroscience; it then
            reviews neuroinformatic toolboxes and libraries, collected from the literature
            and from specific hubs of software projects oriented to neuroscience research.
            The selected tools are presented in tables detailing key features grouped by
            domain of application (e.g. data type, neuroscience area, task), model
            engineering (e.g. programming language, model customization) and technological
            aspect (e.g. interface, code source). The results show that, among a high
            number of available software tools, several libraries are standing out in terms
            of functionalities for neuroscience applications. The aggregation and
            discussion of this information can help the neuroscience community to devolop
            their research projects more efficiently and quickly, both by means of readily
            available tools, and by knowing which modules may be improved, connected or
            added.

            Author name = Louis Fabrice Tshimanga
            Author name = Manfredo Atzori
            Author name = Federico Del Pup
            Author name = Maurizio Corbetta
            Link href = http://arxiv.org/abs/2301.05057v1
            Link href = http://arxiv.org/pdf/2301.05057v1
            primary category term = q-bio.QM
            term = q-bio.QM
            term = cs.LG
            term = cs.NE

            -----------------------------------------------------------------------

            length = 14 (number of items)
            index = 0     item = http://arxiv.org/abs/1709.02325v1
            index = 1     item = 7/09/2017 16:03:48
            index = 2     item = 7/09/2017 16:03:48
            index = 3     item = Multilayer Brain Networks
            index = 4     item =   The field of neuroscience is facing an unprecedented expanse in the volume
            and diversity of available data. Traditionally, network models have provided
            key insights into the structure and function of the brain. With the advent of
            big data in neuroscience, both more sophisticated models capable of
            characterizing the increasing complexity of the data and novel methods of
            quantitative analysis are needed. Recently multilayer networks, a mathematical
            extension of traditional networks, have gained increasing popularity in
            neuroscience due to their ability to capture the full information of
            multi-model, multi-scale, spatiotemporal data sets. Here, we review multilayer
            networks and their applications in neuroscience, showing how incorporating the
            multilayer framework into network neuroscience analysis has uncovered
            previously hidden features of brain networks. We specifically highlight the use
            of multilayer networks to model disease, structure-function relationships,
            network evolution, and link multi-scale data. Finally, we close with a
            discussion of promising new directions of multilayer network neuroscience
            research and propose a modified definition of multilayer networks designed to
            unite and clarify the use of the multilayer formalism in describing real-world
            systems.

            Author name = Michael Vaiana
            Author name = Sarah Muldoon
            index = 7     item = 10.1007/s00332-017-9436-8
            Link href = http://dx.doi.org/10.1007/s00332-017-9436-8
            Link href = http://arxiv.org/abs/1709.02325v1
            Link href = http://arxiv.org/pdf/1709.02325v1
            primary category term = q-bio.NC
            term = q-bio.NC
            term = physics.soc-ph

            -----------------------------------------------------------------------*/

            Console.Read();
        }
    }
}
