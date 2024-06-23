using Microsoft.EntityFrameworkCore;

namespace ConsoleGeopunt30may2024
{
    public class GeopuntDbContext30may2024 : DbContext
    {
        // Add NuGet package Microsoft.EntityFrameworkCore.Sqlite
        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlite($"Data Source=../../../../geopunt30may2024.db");

        public DbSet<Adres> Adressen { get; set; }

        public DbSet<Postcode> Postcodes { get; set; }

        public DbSet<Gemeente> Gemeentes { get; set; }

        public DbSet<Straat> Straten { get; set; }

        public DbSet<GemeenteStraatAssociation> GemeenteStraatAssociations { get; set; }

        public DbSet<Huisnummer> Huisnummers { get; set; }

        public DbSet<StraatHuisnummerAssociation> StraatHuisnummerAssociations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}