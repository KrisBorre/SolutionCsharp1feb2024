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
                if (group.Count() > 1) Console.WriteLine($"{group.Key} ({group.Count()})");
            }

            /*
            Hello, Arxiv database!
            Kazutaka Takahashi (3)
            Jian-da Wu (2)
            Mei-sheng Zhao (2)
            Yong-de Zhang (2)
            J. W. Burby (2)
            Miroslav Pardy (2)
            P. L. Robinson (2)
            Changjing Zhuge (2)
            Jinzhi Lei (2)
            Md. Jahoor Alam (2)
            R. K. Brojen Singh (2)
            Oleg Makarenkov (2)
            Giovanni Gallavotti (2)
            Takayuki Tatekawa (2)
            Jinqiao Duan (3)
            Ziying He (2)
            Gh. Haghighatdoost (2)
            G. Sardanashvily (4)
            Nguyen Tien Zung (2)
            L. Mangiarotti (2)
            T. Djama (2)
            Alain Brizard (2)
            Alain J. Brizard (3)
            Natalia Tronko (2)
            Kotaro Fujisawa (2)
            Jean-Régis Angilella (3)
            Anatoly Neishtadt (3)
            Massimiliano Berti (2)
            Philippe Bolle (2)
            G. A. Grigorian (2)
            A. I. Neishtadt (3)
            A. A. Vasiliev (2)
            A. Osmane (3)
            A. M. Hamza (3)
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
