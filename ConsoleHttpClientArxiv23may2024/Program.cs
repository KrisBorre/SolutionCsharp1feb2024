using System.Xml.Serialization;

namespace ConsoleHttpClientArxiv23may2024
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
            string word = "genetics";

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

            /*
            <?xml version="1.0" encoding="UTF-8"?>
            <feed xmlns="http://www.w3.org/2005/Atom">
              <link href="http://arxiv.org/api/query?search_query%3Dall%3Aadiabatic%26id_list%3D%26start%3D0%26max_results%3D10" rel="self" type="application/atom+xml"/>
              <title type="html">ArXiv Query: search_query=all:adiabatic&amp;id_list=&amp;start=0&amp;max_results=10</title>
              <id>http://arxiv.org/api/4ZCa/9cs7/j7i54vk+qUvCiZi3I</id>
              <updated>2024-06-06T00:00:00-04:00</updated>
              <opensearch:totalResults xmlns:opensearch="http://a9.com/-/spec/opensearch/1.1/">16390</opensearch:totalResults>
              <opensearch:startIndex xmlns:opensearch="http://a9.com/-/spec/opensearch/1.1/">0</opensearch:startIndex>
              <opensearch:itemsPerPage xmlns:opensearch="http://a9.com/-/spec/opensearch/1.1/">10</opensearch:itemsPerPage>
              <entry>
                <id>http://arxiv.org/abs/physics/0203049v1</id>
                <updated>2002-03-15T18:26:02Z</updated>
                <published>2002-03-15T18:26:02Z</published>
                <title>Non-adiabatic coupling and adiabatic population transfer in quantum
              molecular systems</title>
                <summary>  We show that a counter-intuitive pulse sequence leads to adiabatic passage
            between the vibrational levels of three harmonic potentials through parallel
            dark states in adiabatic approximation. However, the adiabatic assumptions
            break down for very intense pulses and non-adiabatic couplings result in the
            population transfer by light-induced potential shaping.
            </summary>
                <author>
                  <name>Ignacio R. Solá</name>
                </author>
                <author>
                  <name>Vladimir S. Malinovsky</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">5 pages, 2 figures</arxiv:comment>
                <link href="http://arxiv.org/abs/physics/0203049v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/physics/0203049v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="physics.chem-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="physics.chem-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="physics.atom-ph" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/1403.6545v1</id>
                <updated>2014-03-26T00:55:02Z</updated>
                <published>2014-03-26T00:55:02Z</published>
                <title>On The Power Of Coherently Controlled Quantum Adiabatic Evolutions</title>
                <summary>  A major challenge facing adiabatic quantum computing is that algorithm design
            and error correction can be difficult for adiabatic quantum computing. Recent
            work has considered addressing his challenge by using coherently controlled
            adiabatic evolutions in the place of classically controlled evolution. An
            important question remains: what is the relative power of controlled adiabatic
            evolution to traditional adiabatic evolutions? We address this by showing that
            coherent control and measurement provides a way to average different adiabatic
            evolutions in ways that cause their diabatic errors to cancel, allowing for
            adiabatic evolutions to combine the best characteristics of existing adiabatic
            optimizations strategies that are mutually exclusive in conventional adiabatic
            QIP. This result shows that coherent control and measurement can provide
            advantages for adiabatic state preparation. We also provide upper bounds on the
            complexity of simulating such evolutions on a circuit based quantum computer
            and provide sufficiency conditions for the equivalence of controlled adiabatic
            evolutions to adiabatic quantum computing.
            </summary>
                <author>
                  <name>Maria Kieferova</name>
                </author>
                <author>
                  <name>Nathan Wiebe</name>
                </author>
                <arxiv:doi xmlns:arxiv="http://arxiv.org/schemas/atom">10.1088/1367-2630/16/12/123034</arxiv:doi>
                <link title="doi" href="http://dx.doi.org/10.1088/1367-2630/16/12/123034" rel="related"/>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">20 pages, 7 figures</arxiv:comment>
                <link href="http://arxiv.org/abs/1403.6545v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1403.6545v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/quant-ph/0701135v2</id>
                <updated>2007-02-17T02:36:15Z</updated>
                <published>2007-01-19T01:22:13Z</published>
                <title>Linearity and Quantum Adiabatic Theorem</title>
                <summary>  We show that in a quantum adiabatic evolution, even though the adiabatic
            approximation is valid, the total phase of the final state indicated by the
            adiabatic theorem may evidently differ from the actual total phase. This
            invalidates the application of the linearity and the adiabatic approximation
            simultaneously. Besides, based on this observation we point out the mistake in
            the traditional proof for the adiabatic theorem. This mistake is the root of
            the troubles that the adiabatic theorem has met. We also show that a similar
            mistake remains in some recent modifications of the traditional adiabatic
            theorem attempting to eliminate the troubles.
            </summary>
                <author>
                  <name>Zhaohui Wei</name>
                </author>
                <author>
                  <name>Mingsheng Ying</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">4 pages, 1 figure. comments are welcome</arxiv:comment>
                <link href="http://arxiv.org/abs/quant-ph/0701135v2" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/quant-ph/0701135v2" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/1204.0344v1</id>
                <updated>2012-04-02T08:56:11Z</updated>
                <published>2012-04-02T08:56:11Z</published>
                <title>Non-adiabatic transitions in a massless scalar field</title>
                <summary>  We consider the dynamics of a massless scalar field with time-dependent
            sources in the adiabatic limit. This is an example of an adiabatic problem
            without spectral gap. The main goal of our paper is to illustrate the
            difference between the error of the adiabatic approximation and the concept of
            non-adiabatic transitions for gapless systems. In our example the non-adiabatic
            transitions correspond to emission of free bosons, while the error of the
            adiabatic approximation is dominated by a velocity-dependent deformation of the
            ground state of the field. In order to capture these concepts precisely, we
            show how to construct super-adiabatic approximations for a gapless system.
            </summary>
                <author>
                  <name>Johannes von Keler</name>
                </author>
                <author>
                  <name>Stefan Teufel</name>
                </author>
                <link href="http://arxiv.org/abs/1204.0344v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1204.0344v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="math-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="math-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="math.MP" scheme="http://arxiv.org/schemas/atom"/>
                <category term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="81V10, 81Q99" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/0901.4954v1</id>
                <updated>2009-01-30T18:55:34Z</updated>
                <published>2009-01-30T18:55:34Z</published>
                <title>A note on adiabatic theorem for Markov chains and adiabatic quantum
              computation</title>
                <summary>  We derive an adiabatic theorem for Markov chains using well known facts about
            mixing and relaxation times. We discuss the results in the context of the
            recent developments in adiabatic quantum computation.
            </summary>
                <author>
                  <name>Yevgeniy Kovchegov</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">9 pages</arxiv:comment>
                <link href="http://arxiv.org/abs/0901.4954v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/0901.4954v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="math.PR" scheme="http://arxiv.org/schemas/atom"/>
                <category term="math.PR" scheme="http://arxiv.org/schemas/atom"/>
                <category term="60G35" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/quant-ph/0304130v2</id>
                <updated>2004-02-09T23:12:09Z</updated>
                <published>2003-04-21T19:53:47Z</published>
                <title>Perturbative Formulation and Non-adiabatic Corrections in Adiabatic
              Quantum Computing Schemes</title>
                <summary>  Adiabatic limit is the presumption of the adiabatic geometric quantum
            computation and of the adiabatic quantum algorithm. But in reality, the
            variation speed of the Hamiltonian is finite. Here we develop a general
            formulation of adiabatic quantum computing, which accurately describes the
            evolution of the quantum state in a perturbative way, in which the adiabatic
            limit is the zeroth-order approximation. As an application of this formulation,
            non-adiabatic correction or error is estimated for several physical
            implementations of the adiabatic geometric gates. A quantum computing process
            consisting of many adiabatic gate operations is considered, for which the total
            non-adiabatic error is found to be about the sum of those of all the gates.
            This is a useful constraint on the computational power. The formalism is also
            briefly applied to the adiabatic quantum algorithm.
            </summary>
                <author>
                  <name>Yu Shi</name>
                </author>
                <author>
                  <name>Yong-Shi Wu</name>
                </author>
                <arxiv:doi xmlns:arxiv="http://arxiv.org/schemas/atom">10.1103/PhysRevA.69.024301</arxiv:doi>
                <link title="doi" href="http://dx.doi.org/10.1103/PhysRevA.69.024301" rel="related"/>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">5 pages, revtex. some references added</arxiv:comment>
                <arxiv:journal_ref xmlns:arxiv="http://arxiv.org/schemas/atom">Phy. Rev. A 69, 024301 (2004)</arxiv:journal_ref>
                <link href="http://arxiv.org/abs/quant-ph/0304130v2" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/quant-ph/0304130v2" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="cond-mat" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/1701.04662v1</id>
                <updated>2017-01-17T13:23:21Z</updated>
                <published>2017-01-17T13:23:21Z</published>
                <title>Adiabatic Processes for Charged AdS Black Hole in the Extended Phase
              Space</title>
                <summary>  In the extended phase space, a general method is used to derive all the
            possible adiabatic processes for charged AdS black hole. Two kinds are found,
            one is zero temperature adiabatic process which is irreversible, the other is
            isochore adiabatic process which is reversible. For the zero temperature
            adiabatic expansion process, entropy is increasing; pressure, enthalpy, Gibbs
            free energy and internal energy are decreasing; system's potential energy is
            transformed to the work done by the system to the outer system. For the
            isochore adiabatic process, entropy and internal energy are fixed; temperature,
            enthalpy and Gibbs free energy are proportional to pressure; during the
            pressure increasing process, temperature is increasing and system's potential
            energy is transformed to its kinetic energy. Comparing these two adiabatic
            processes with those in normal thermodynamic system, we find that the zero
            temperature adiabatic process is much like the adiabatic throttling
            process(both are irreversible and with work done), the isochore adiabatic
            process is much like a combination of the reversible adiabatic process (both
            with fixed entropy) and the adiabatic free expansion process (both with fixed
            internal energy).
            </summary>
                <author>
                  <name>Shanquan Lan</name>
                </author>
                <author>
                  <name>Wenbiao Liu</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">6pages,2figures</arxiv:comment>
                <link href="http://arxiv.org/abs/1701.04662v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/1701.04662v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="hep-th" scheme="http://arxiv.org/schemas/atom"/>
                <category term="hep-th" scheme="http://arxiv.org/schemas/atom"/>
                <category term="gr-qc" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/2010.05550v1</id>
                <updated>2020-10-12T09:15:35Z</updated>
                <published>2020-10-12T09:15:35Z</published>
                <title>Controlling and exploring quantum systems by algebraic expression of
              adiabatic gauge potential</title>
                <summary>  Adiabatic gauge potential is the origin of nonadiabatic transitions. In
            counterdiabatic driving, which is a method of shortcuts to adiabaticity,
            adiabatic gauge potential can be used to realize identical dynamics to
            adiabatic time evolution without requiring slow change of parameters. We
            introduce an algebraic expression of adiabatic gauge potential. Then, we find
            that the explicit form of adiabatic gauge potential can be easily determined by
            some algebraic calculations. We demonstrate this method by using a single-spin
            system, a two-spin system, and the transverse Ising chain. Moreover, we derive
            a lower bound for fidelity to adiabatic time evolution based on the quantum
            speed limit. This bound enables us to know the worst case performance of
            approximate adiabatic gauge potential. We can also use this bound to find
            dominant terms in adiabatic gauge potential to suppress nonadiabatic
            transitions. We apply this bound to magnetization reversal of the two-spin
            system and to quantum annealing of the transverse Ising chain. Adiabatic gauge
            potential reflects structure of energy eigenstates, and thus we also discuss
            detection of quantum phase transitions by using adiabatic gauge potential. We
            find a signature of a quantum phase transition in the transverse Ising chain.
            </summary>
                <author>
                  <name>Takuya Hatomura</name>
                </author>
                <author>
                  <name>Kazutaka Takahashi</name>
                </author>
                <arxiv:doi xmlns:arxiv="http://arxiv.org/schemas/atom">10.1103/PhysRevA.103.012220</arxiv:doi>
                <link title="doi" href="http://dx.doi.org/10.1103/PhysRevA.103.012220" rel="related"/>
                <arxiv:journal_ref xmlns:arxiv="http://arxiv.org/schemas/atom">Phys. Rev. A 103, 012220 (2021)</arxiv:journal_ref>
                <link href="http://arxiv.org/abs/2010.05550v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/2010.05550v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="cond-mat.stat-mech" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/2010.14667v1</id>
                <updated>2020-10-27T23:34:48Z</updated>
                <published>2020-10-27T23:34:48Z</published>
                <title>The adiabatic limit of Fu-Yau equations</title>
                <summary>  In this paper, we consider the adiabatic limit of Fu-Yau equations on a
            product of two Calabi-Yau manifolds. We prove that the adiabatic limit of
            Fu-Yau equations are quasilinear equations.
            </summary>
                <author>
                  <name>Liding Huang</name>
                </author>
                <link href="http://arxiv.org/abs/2010.14667v1" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/2010.14667v1" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="math.AP" scheme="http://arxiv.org/schemas/atom"/>
                <category term="math.AP" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
              <entry>
                <id>http://arxiv.org/abs/0706.0264v2</id>
                <updated>2007-06-05T02:40:56Z</updated>
                <published>2007-06-02T11:04:33Z</published>
                <title>Adiabatic Approximation Condition</title>
                <summary>  In this paper, we present an invariant perturbation theory of the adiabatic
            process based on the concepts of U(1)-invariant adiabatic orbit and
            U(1)-invariant adiabatic expansion. As its application, we propose and discuss
            new adiabatic approximation conditions.
            </summary>
                <author>
                  <name>Jian-da Wu</name>
                </author>
                <author>
                  <name>Mei-sheng Zhao</name>
                </author>
                <author>
                  <name>Jian-lan Chen</name>
                </author>
                <author>
                  <name>Yong-de Zhang</name>
                </author>
                <arxiv:comment xmlns:arxiv="http://arxiv.org/schemas/atom">4 pages, no figures</arxiv:comment>
                <link href="http://arxiv.org/abs/0706.0264v2" rel="alternate" type="text/html"/>
                <link title="pdf" href="http://arxiv.org/pdf/0706.0264v2" rel="related" type="application/pdf"/>
                <arxiv:primary_category xmlns:arxiv="http://arxiv.org/schemas/atom" term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
                <category term="quant-ph" scheme="http://arxiv.org/schemas/atom"/>
              </entry>
            </feed>
            */

            /*
            title value = ArXiv Query: search_query=all:adiabatic&id_list=&start=0&max_results=10
            length = 10
            length = 13
            index = 0     item = http://arxiv.org/abs/physics/0203049v1
            index = 1     item = 15/03/2002 18:26:02
            index = 2     item = 15/03/2002 18:26:02
            index = 3     item = Non-adiabatic coupling and adiabatic population transfer in quantum
              molecular systems
            index = 4     item =   We show that a counter-intuitive pulse sequence leads to adiabatic passage
            between the vibrational levels of three harmonic potentials through parallel
            dark states in adiabatic approximation. However, the adiabatic assumptions
            break down for very intense pulses and non-adiabatic couplings result in the
            population transfer by light-induced potential shaping.

            Author name = Ignacio R. Solá
            Author name = Vladimir S. Malinovsky
            index = 7     item = 5 pages, 2 figures
            Link href = http://arxiv.org/abs/physics/0203049v1
            Link href = http://arxiv.org/pdf/physics/0203049v1
            primary category term = physics.chem-ph
            term = physics.chem-ph
            term = physics.atom-ph

            -----------------------------------------------------------------------

            length = 14
            index = 0     item = http://arxiv.org/abs/1403.6545v1
            index = 1     item = 26/03/2014 0:55:02
            index = 2     item = 26/03/2014 0:55:02
            index = 3     item = On The Power Of Coherently Controlled Quantum Adiabatic Evolutions
            index = 4     item =   A major challenge facing adiabatic quantum computing is that algorithm design
            and error correction can be difficult for adiabatic quantum computing. Recent
            work has considered addressing his challenge by using coherently controlled
            adiabatic evolutions in the place of classically controlled evolution. An
            important question remains: what is the relative power of controlled adiabatic
            evolution to traditional adiabatic evolutions? We address this by showing that
            coherent control and measurement provides a way to average different adiabatic
            evolutions in ways that cause their diabatic errors to cancel, allowing for
            adiabatic evolutions to combine the best characteristics of existing adiabatic
            optimizations strategies that are mutually exclusive in conventional adiabatic
            QIP. This result shows that coherent control and measurement can provide
            advantages for adiabatic state preparation. We also provide upper bounds on the
            complexity of simulating such evolutions on a circuit based quantum computer
            and provide sufficiency conditions for the equivalence of controlled adiabatic
            evolutions to adiabatic quantum computing.

            Author name = Maria Kieferova
            Author name = Nathan Wiebe
            index = 7     item = 10.1088/1367-2630/16/12/123034
            Link href = http://dx.doi.org/10.1088/1367-2630/16/12/123034
            index = 9     item = 20 pages, 7 figures
            Link href = http://arxiv.org/abs/1403.6545v1
            Link href = http://arxiv.org/pdf/1403.6545v1
            primary category term = quant-ph
            term = quant-ph

            -----------------------------------------------------------------------

            length = 12
            index = 0     item = http://arxiv.org/abs/quant-ph/0701135v2
            index = 1     item = 17/02/2007 2:36:15
            index = 2     item = 19/01/2007 1:22:13
            index = 3     item = Linearity and Quantum Adiabatic Theorem
            index = 4     item =   We show that in a quantum adiabatic evolution, even though the adiabatic
            approximation is valid, the total phase of the final state indicated by the
            adiabatic theorem may evidently differ from the actual total phase. This
            invalidates the application of the linearity and the adiabatic approximation
            simultaneously. Besides, based on this observation we point out the mistake in
            the traditional proof for the adiabatic theorem. This mistake is the root of
            the troubles that the adiabatic theorem has met. We also show that a similar
            mistake remains in some recent modifications of the traditional adiabatic
            theorem attempting to eliminate the troubles.

            Author name = Zhaohui Wei
            Author name = Mingsheng Ying
            index = 7     item = 4 pages, 1 figure. comments are welcome
            Link href = http://arxiv.org/abs/quant-ph/0701135v2
            Link href = http://arxiv.org/pdf/quant-ph/0701135v2
            primary category term = quant-ph
            term = quant-ph

            -----------------------------------------------------------------------

            length = 14
            index = 0     item = http://arxiv.org/abs/1204.0344v1
            index = 1     item = 2/04/2012 8:56:11
            index = 2     item = 2/04/2012 8:56:11
            index = 3     item = Non-adiabatic transitions in a massless scalar field
            index = 4     item =   We consider the dynamics of a massless scalar field with time-dependent
            sources in the adiabatic limit. This is an example of an adiabatic problem
            without spectral gap. The main goal of our paper is to illustrate the
            difference between the error of the adiabatic approximation and the concept of
            non-adiabatic transitions for gapless systems. In our example the non-adiabatic
            transitions correspond to emission of free bosons, while the error of the
            adiabatic approximation is dominated by a velocity-dependent deformation of the
            ground state of the field. In order to capture these concepts precisely, we
            show how to construct super-adiabatic approximations for a gapless system.

            Author name = Johannes von Keler
            Author name = Stefan Teufel
            Link href = http://arxiv.org/abs/1204.0344v1
            Link href = http://arxiv.org/pdf/1204.0344v1
            primary category term = math-ph
            term = math-ph
            term = math.MP
            term = quant-ph
            term = 81V10, 81Q99

            -----------------------------------------------------------------------

            length = 12
            index = 0     item = http://arxiv.org/abs/0901.4954v1
            index = 1     item = 30/01/2009 18:55:34
            index = 2     item = 30/01/2009 18:55:34
            index = 3     item = A note on adiabatic theorem for Markov chains and adiabatic quantum
              computation
            index = 4     item =   We derive an adiabatic theorem for Markov chains using well known facts about
            mixing and relaxation times. We discuss the results in the context of the
            recent developments in adiabatic quantum computation.

            Author name = Yevgeniy Kovchegov
            index = 6     item = 9 pages
            Link href = http://arxiv.org/abs/0901.4954v1
            Link href = http://arxiv.org/pdf/0901.4954v1
            primary category term = math.PR
            term = math.PR
            term = 60G35

            -----------------------------------------------------------------------

            length = 16
            index = 0     item = http://arxiv.org/abs/quant-ph/0304130v2
            index = 1     item = 9/02/2004 23:12:09
            index = 2     item = 21/04/2003 19:53:47
            index = 3     item = Perturbative Formulation and Non-adiabatic Corrections in Adiabatic
              Quantum Computing Schemes
            index = 4     item =   Adiabatic limit is the presumption of the adiabatic geometric quantum
            computation and of the adiabatic quantum algorithm. But in reality, the
            variation speed of the Hamiltonian is finite. Here we develop a general
            formulation of adiabatic quantum computing, which accurately describes the
            evolution of the quantum state in a perturbative way, in which the adiabatic
            limit is the zeroth-order approximation. As an application of this formulation,
            non-adiabatic correction or error is estimated for several physical
            implementations of the adiabatic geometric gates. A quantum computing process
            consisting of many adiabatic gate operations is considered, for which the total
            non-adiabatic error is found to be about the sum of those of all the gates.
            This is a useful constraint on the computational power. The formalism is also
            briefly applied to the adiabatic quantum algorithm.

            Author name = Yu Shi
            Author name = Yong-Shi Wu
            index = 7     item = 10.1103/PhysRevA.69.024301
            Link href = http://dx.doi.org/10.1103/PhysRevA.69.024301
            index = 9     item = 5 pages, revtex. some references added
            index = 10     item = Phy. Rev. A 69, 024301 (2004)
            Link href = http://arxiv.org/abs/quant-ph/0304130v2
            Link href = http://arxiv.org/pdf/quant-ph/0304130v2
            primary category term = quant-ph
            term = quant-ph
            term = cond-mat

            -----------------------------------------------------------------------

            length = 13
            index = 0     item = http://arxiv.org/abs/1701.04662v1
            index = 1     item = 17/01/2017 13:23:21
            index = 2     item = 17/01/2017 13:23:21
            index = 3     item = Adiabatic Processes for Charged AdS Black Hole in the Extended Phase
              Space
            index = 4     item =   In the extended phase space, a general method is used to derive all the
            possible adiabatic processes for charged AdS black hole. Two kinds are found,
            one is zero temperature adiabatic process which is irreversible, the other is
            isochore adiabatic process which is reversible. For the zero temperature
            adiabatic expansion process, entropy is increasing; pressure, enthalpy, Gibbs
            free energy and internal energy are decreasing; system's potential energy is
            transformed to the work done by the system to the outer system. For the
            isochore adiabatic process, entropy and internal energy are fixed; temperature,
            enthalpy and Gibbs free energy are proportional to pressure; during the
            pressure increasing process, temperature is increasing and system's potential
            energy is transformed to its kinetic energy. Comparing these two adiabatic
            processes with those in normal thermodynamic system, we find that the zero
            temperature adiabatic process is much like the adiabatic throttling
            process(both are irreversible and with work done), the isochore adiabatic
            process is much like a combination of the reversible adiabatic process (both
            with fixed entropy) and the adiabatic free expansion process (both with fixed
            internal energy).

            Author name = Shanquan Lan
            Author name = Wenbiao Liu
            index = 7     item = 6pages,2figures
            Link href = http://arxiv.org/abs/1701.04662v1
            Link href = http://arxiv.org/pdf/1701.04662v1
            primary category term = hep-th
            term = hep-th
            term = gr-qc

            -----------------------------------------------------------------------

            length = 15
            index = 0     item = http://arxiv.org/abs/2010.05550v1
            index = 1     item = 12/10/2020 9:15:35
            index = 2     item = 12/10/2020 9:15:35
            index = 3     item = Controlling and exploring quantum systems by algebraic expression of
              adiabatic gauge potential
            index = 4     item =   Adiabatic gauge potential is the origin of nonadiabatic transitions. In
            counterdiabatic driving, which is a method of shortcuts to adiabaticity,
            adiabatic gauge potential can be used to realize identical dynamics to
            adiabatic time evolution without requiring slow change of parameters. We
            introduce an algebraic expression of adiabatic gauge potential. Then, we find
            that the explicit form of adiabatic gauge potential can be easily determined by
            some algebraic calculations. We demonstrate this method by using a single-spin
            system, a two-spin system, and the transverse Ising chain. Moreover, we derive
            a lower bound for fidelity to adiabatic time evolution based on the quantum
            speed limit. This bound enables us to know the worst case performance of
            approximate adiabatic gauge potential. We can also use this bound to find
            dominant terms in adiabatic gauge potential to suppress nonadiabatic
            transitions. We apply this bound to magnetization reversal of the two-spin
            system and to quantum annealing of the transverse Ising chain. Adiabatic gauge
            potential reflects structure of energy eigenstates, and thus we also discuss
            detection of quantum phase transitions by using adiabatic gauge potential. We
            find a signature of a quantum phase transition in the transverse Ising chain.

            Author name = Takuya Hatomura
            Author name = Kazutaka Takahashi
            index = 7     item = 10.1103/PhysRevA.103.012220
            Link href = http://dx.doi.org/10.1103/PhysRevA.103.012220
            index = 9     item = Phys. Rev. A 103, 012220 (2021)
            Link href = http://arxiv.org/abs/2010.05550v1
            Link href = http://arxiv.org/pdf/2010.05550v1
            primary category term = quant-ph
            term = quant-ph
            term = cond-mat.stat-mech

            -----------------------------------------------------------------------

            length = 10
            index = 0     item = http://arxiv.org/abs/2010.14667v1
            index = 1     item = 27/10/2020 23:34:48
            index = 2     item = 27/10/2020 23:34:48
            index = 3     item = The adiabatic limit of Fu-Yau equations
            index = 4     item =   In this paper, we consider the adiabatic limit of Fu-Yau equations on a
            product of two Calabi-Yau manifolds. We prove that the adiabatic limit of
            Fu-Yau equations are quasilinear equations.

            Author name = Liding Huang
            Link href = http://arxiv.org/abs/2010.14667v1
            Link href = http://arxiv.org/pdf/2010.14667v1
            primary category term = math.AP
            term = math.AP

            -----------------------------------------------------------------------

            length = 14
            index = 0     item = http://arxiv.org/abs/0706.0264v2
            index = 1     item = 5/06/2007 2:40:56
            index = 2     item = 2/06/2007 11:04:33
            index = 3     item = Adiabatic Approximation Condition
            index = 4     item =   In this paper, we present an invariant perturbation theory of the adiabatic
            process based on the concepts of U(1)-invariant adiabatic orbit and
            U(1)-invariant adiabatic expansion. As its application, we propose and discuss
            new adiabatic approximation conditions.

            Author name = Jian-da Wu
            Author name = Mei-sheng Zhao
            Author name = Jian-lan Chen
            Author name = Yong-de Zhang
            index = 9     item = 4 pages, no figures
            Link href = http://arxiv.org/abs/0706.0264v2
            Link href = http://arxiv.org/pdf/0706.0264v2
            primary category term = quant-ph
            term = quant-ph

            -----------------------------------------------------------------------

            */

            Console.Read();
        }
    }
}
