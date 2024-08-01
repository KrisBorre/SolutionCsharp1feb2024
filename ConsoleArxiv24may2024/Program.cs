using LibraryArxiv24may2024;
using Microsoft.EntityFrameworkCore;

namespace ConsoleArxiv24may2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Arxiv database!");

            ArxivDbContext23may2024 dbContext = new ArxivDbContext23may2024();
            dbContext.Database.EnsureCreated();

            string word1 = "adiabatic";
            string word2 = "invariant";

            // 963 abstracts
            List<Article> allArticles = dbContext.Articles
                .Include(a => a.Contributions)
                .Include(a => a.Links)
                .ToList();

            Console.WriteLine("Number of Articles: " + allArticles.Count); // 1531

            List<Contribution> allContributions = dbContext.Contributions.ToList();
            List<Link> allLinks = dbContext.Links.ToList();

            //var selectedArticles1 = allArticles.FindAll(x => x.Title.Contains(word1)); // 32

            //var selectedArticles2 = allArticles.FindAll(x => x.Title.Contains(word2)); // 23

            var grouped = allContributions.GroupBy(c => c.Author);

            foreach (var group in grouped)
            {
                if (group.Count() > 6) Console.WriteLine($"{group.Key} ({group.Count()})");
            }
            /*
A. I. Neishtadt (8)
Jérôme Darmont (16)
Fadila Bentayeb (7)
T. Mart (10)
James Atwood (7)
            */

            /*
            J. W. Burby (4)
            G. Sardanashvily (4)
            Alain J. Brizard (5)
            A. P. Itin (4)
            A. I. Neishtadt (8)
            A. A. Vasiliev (5)
            Danielle S. Bassett (4)
            Nick Patterson (4)
            David Reich (4)
            */

            int earlier = allArticles.FindAll(a => a.DateTime1 < a.DateTime2).Count;
            Console.WriteLine("earlier = " + earlier);
            int same = allArticles.FindAll(a => a.DateTime1 == a.DateTime2).Count;
            Console.WriteLine("same = " + same);
            int later = allArticles.FindAll(a => a.DateTime1 > a.DateTime2).Count; //submission date? // announcement date?
            Console.WriteLine("later = " + later);

            /*
            earlier = 0
            same = 566
            later = 199
            */

            List<Article> githubArticles = allArticles.FindAll(a => a.Abstract.Contains("github"));

            foreach (var article in githubArticles)
            {
                string[] titles = article.Title.Split("\n");
                Console.WriteLine(titles[0]);
                Console.WriteLine(article.Abstract + "\n");
            }

            /*           
            DGoT: Dynamic Graph of Thoughts for Scientific Abstract Generation
            https://github.com/JayceNing/DGoT
            Python

            Large Language Models are Fixated by Red Herrings: Exploring Creative
            https://github.com/TaatiTeam/OCW
            Python
            
            MORE: Simultaneous Multi-View 3D Object Recognition and Pose Estimation
            https://github.com/SubhadityaMukherjee/more_mvcnn
            Python

            Putting visual object recognition in context
            https://github.com/kreimanlab/Put-In-Context
            Matlab

            Unsupervised Domain Attention Adaptation Network for Caricature
            https://github.com/KeleiHe/DAAN
            Python

            Genome-on-Diet: Taming Large-Scale Genomic Analyses via Sparsified
            https://github.com/CMU-SAFARI/Genome-on-Diet
            Roff

            Identifying recombination hotspots using population genetic data
            http://github.com/auton1/LDhot
            C++
            */

            Console.WriteLine();

            #region abstract uses the words from the title
            //Console.WriteLine("\n\nLet's check if the abstract and the title use the same words.");
            //int number_of_articles = 0;
            //foreach (Article article in allArticles)
            //{
            //    string[] array = article.Title.Split(' ');
            //    int used = 0;

            //    foreach (var titleWord in array)
            //    {
            //        if (article.Abstract.Contains(titleWord, StringComparison.InvariantCultureIgnoreCase))
            //        {
            //            used++;
            //        }
            //    }

            //    double ratio = used / ((double)array.Length);

            //    if (ratio == 1.0)
            //    {
            //        Console.WriteLine(article.Title);
            //        number_of_articles++;
            //    }
            //}

            //Console.WriteLine(number_of_articles / ((double)allArticles.Count));
            //// 1.0 -> 0,199 // 0.95 -> 0,201 // 0.90 -> 0,271 // 0.85 -> 0,415 // 0.80 -> 0,511        

            //Console.WriteLine("\n20 percent of abstracts contain all the title words.");

            //Console.WriteLine("27 percent of abstracts contain 90 percent of the title words.");

            //Console.WriteLine("41 percent of abstracts contain 85 percent of the title words.");

            //Console.WriteLine("51 percent of abstracts contain 80 percent of the title words.\n");
            #endregion

            #region abstracts usually don't use references

            //int brackets = 0;

            //foreach (Article article in allArticles)
            //{
            //    if ((article.Abstract.Contains("(1998") || article.Abstract.Contains("(201")) && article.Abstract.Contains(')'))
            //    {
            //        brackets++;
            //        Console.WriteLine(article.Abstract);
            //    }
            //}

            //Console.WriteLine(brackets / ((double)allArticles.Count));

            /*
            We present the second-order phase transition from a band insulator to metal
            that is induced by a strong magnetic field. The magnetic-field dependences of
            the magnetization and energy band gap of a crystalline silicon immersed in a
            magnetic field are investigated by means of the nonperturbative
            magnetic-field-containing relativistic tight-binding approximation method
            [Phys. Rev. B 97, 195135 (2018)].

            This paper discusses asymptotic theory for penalized spline estimators in
            generalized additive models. The purpose of this paper is to establish the
            asymptotic bias and variance as well as the asymptotic normality of the
            penalized spline estimators proposed by Marx and Eilers (1998).

            A time dependence of a metal abundance of supernova remnant (SNR) have been
            found by Hughes et al. (1998).

            The time-dependent surface flux method developed for the description of
            electronic spectra [L. Tao and A. Scrinzi, New J. Phys. 14, 013021 (2012); A.
            Scrinzi, New J. Phys. 14, 085008 (2012)] is extended to treat dissociation and
            dissociative ionization processes of H2+ interacting with strong laser pulses.

            A range of a priori hypotheses about the evolution of modern and archaic
            genomes are further evaluated and tested. In addition to the well-known
            splits/introgressions involving Neanderthal genes into out-of- Africa people,
            or Denisovan genes into Oceanians, a further series of archaic splits and
            hypotheses proposed in Waddell et al. (2011) are considered in detail.

            In a recent paper, Hirsch (2018) proposes to attribute the credit for a
            co-authored paper to the {\alpha}-author--the author with the highest
            h-index--regardless of his or her actual contribution, effectively reducing the
            role of the other co-authors to zero.

            Dispersal of species to find a more favorable habitat is important in
            population dynamics. Dispersal rates evolve in response to the relative success
            of different dispersal strategies. In a simplified deterministic treatment (J.
            Dockery, V. Hutson, K. Mischaikow, et al., J. Math. Bio. 37, 61 (1998)) of two
            species which differ only in their dispersal rates the slow species always
            dominates.

            It has recently been shown that a parametrically driven oscillator with Kerr
            nonlinearity yields a Schr\"odinger cat state via quantum adiabatic evolution
            through its bifurcation point and a network of such nonlinear oscillators can
            be used for solving combinatorial optimization problems by bifurcation-based
            adiabatic quantum computation [H. Goto, Sci. Rep. \textbf{6}, 21686 (2016)].
            */

            //Console.WriteLine("\nOnly 1 percent of abstracts use references.");
            //Console.WriteLine("99 percent of abstracts use no references.");
            #endregion

            #region agricultur and crop
            //List<Article> agriculture = allArticles
            //    .Where(a => a.Title.Contains("agricultur", StringComparison.OrdinalIgnoreCase) ||
            //                a.Title.Contains(" crop ", StringComparison.OrdinalIgnoreCase))
            //    .ToList();

            //foreach (Article article in agriculture) // 41 articles
            //{
            //    string title = article.Title.ReplaceLineEndings(" ");
            //    Console.WriteLine(title);
            //    Link? link = article.Links.Where(l => l.Hyperlink.Contains("pdf", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            //    Console.WriteLine(link?.Hyperlink);
            //}
            #endregion

            List<Article> questionsInAbstractAndTitle = new List<Article>();
            List<Article> questionsInAbstract = new List<Article>();
            List<Article> questionsInTitle = new List<Article>();

            foreach (Article article in allArticles)
            {
                if (article.Title.Contains('?'))
                {
                    questionsInTitle.Add(article);
                }

                if (article.Abstract.Contains('?'))
                {
                    questionsInAbstract.Add(article);
                }

                if (article.Title.Contains('?') && article.Abstract.Contains('?'))
                {
                    questionsInAbstractAndTitle.Add(article);
                }
            }

            Console.WriteLine("Number of articles with questions in the Title: " + questionsInTitle.Count); // 28
            Console.WriteLine("Number of articles with questions in the Abstract: " + questionsInAbstract.Count); // 31
            Console.WriteLine("Number of articles with questions in the Abstract and the Title: " + questionsInAbstractAndTitle.Count); // 2
            Console.WriteLine();

            foreach (Article article in questionsInTitle)
            {
                Console.WriteLine(article.Title);
                Console.WriteLine();
            }

            Console.WriteLine();

            foreach (Article article in questionsInAbstract)
            {
                Console.WriteLine(article.Abstract);
                Console.WriteLine();
            }

            Console.Read();
        }

    }
}
