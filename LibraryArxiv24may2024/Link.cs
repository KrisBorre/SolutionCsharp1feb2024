using System.ComponentModel.DataAnnotations;

namespace LibraryArxiv24may2024
{
    public class Link
    {
        public Link()
        {

        }

        [Key]
        public int LinkID { get; set; }

        public int ArticleID { get; set; }

        public string Hyperlink { get; set; } = string.Empty;
    }
}