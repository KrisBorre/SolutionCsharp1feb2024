using System.ComponentModel.DataAnnotations;

namespace ConsoleHttpClientArxiv23may2024
{
    public class Contribution
    {
        public Contribution()
        {

        }

        [Key]
        public int ContributionID { get; set; }

        public int ArticleID { get; set; }

        public string Author { get; set; } = string.Empty;
    }
}