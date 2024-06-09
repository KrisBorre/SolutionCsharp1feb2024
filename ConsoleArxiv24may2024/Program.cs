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

            List<Contribution> allContributions = dbContext.Contributions.ToList();
            List<Link> allLinks = dbContext.Links.ToList();

            var selectedArticles1 = allArticles.FindAll(x => x.Title.Contains(word1));

            var selectedArticles2 = allArticles.FindAll(x => x.Title.Contains(word2));

            var grouped = allContributions.GroupBy(c => c.Author);

            foreach (var group in grouped)
            {
                if (group.Count() > 3) Console.WriteLine($"{group.Key} ({group.Count()})");
            }

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

            Console.Read();
        }
    }
}
