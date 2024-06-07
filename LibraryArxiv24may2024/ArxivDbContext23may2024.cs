using Microsoft.EntityFrameworkCore;

namespace LibraryArxiv24may2024
{
    public class ArxivDbContext23may2024 : DbContext
    {
        // Add NuGet package Microsoft.EntityFrameworkCore.Sqlite
        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite($"Data Source=../../../../arxiv30may2024.db");

        public DbSet<Article> Articles { get; set; }

        public DbSet<Contribution> Contributions { get; set; }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}