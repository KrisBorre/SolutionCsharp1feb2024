﻿using System.ComponentModel.DataAnnotations;

namespace LibraryArxiv24may2024
{
    public class Article
    {
        public Article()
        {
            Contributions = new List<Contribution>();
            Links = new List<Link>();
        }

        [Key]
        public int ArticleID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Abstract { get; set; } = string.Empty;

        public List<Contribution> Contributions { get; set; }

        public List<Link> Links { get; set; }

        public DateTime DateTime1 { get; set; } // submission date?

        public DateTime DateTime2 { get; set; } // announcement date?
    }
}
