using Microsoft.EntityFrameworkCore;

namespace LibraryDatabaseDictionary25may2024
{
    public class DictionaryDbContext24may2024 : DbContext
    {
        // Add NuGet package Microsoft.EntityFrameworkCore.Sqlite
        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite($"Data Source=../../../../dictionary29may2024.db");

        public DbSet<EnglishWord> Words { get; set; }

        public DbSet<EnglishMeaning> Meanings { get; set; }

        public DbSet<EnglishDefinition> Definitions { get; set; }

        public DbSet<EnglishSynonym> Synonyms { get; set; }

        public DbSet<EnglishAntonym> Antonyms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnglishWord>()
                .HasKey(w => w.WordID);

            modelBuilder.Entity<EnglishWord>()
                .Property(w => w.Word)
                .IsRequired();


            modelBuilder.Entity<EnglishMeaning>()
               .HasKey(m => m.MeaningID);

            modelBuilder.Entity<EnglishMeaning>()
                .Property(m => m.WordID)
                .IsRequired();


            modelBuilder.Entity<EnglishSynonym>()
                .HasKey(s => s.SynonymID);

            modelBuilder.Entity<EnglishSynonym>()
                .Property(s => s.Synonym)
                .IsRequired();

            modelBuilder.Entity<EnglishSynonym>()
                .Property(s => s.MeaningID)
                .IsRequired();


            modelBuilder.Entity<EnglishAntonym>()
                .HasKey(a => a.AntonymID);

            modelBuilder.Entity<EnglishAntonym>()
                .Property(a => a.Antonym)
                .IsRequired();

            modelBuilder.Entity<EnglishAntonym>()
                .Property(a => a.MeaningID)
                .IsRequired();


            modelBuilder.Entity<EnglishDefinition>()
                .HasKey(d => d.DefinitionID);

            modelBuilder.Entity<EnglishDefinition>()
                .Property(d => d.Definition)
                .IsRequired();

            modelBuilder.Entity<EnglishDefinition>()
                .Property(d => d.MeaningID)
                .IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}
