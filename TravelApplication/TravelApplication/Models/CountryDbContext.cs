using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace TravelApplication.Models
{
    public class CountryDbContext:DbContext
    {
        public DbSet<Country_Name> Country_Names { get; set; }
        public DbSet<Country_Image> Country_Images { get; set; }
        public CountryDbContext(DbContextOptions<CountryDbContext> options)
    : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=OUZKAN\\SQLEXPRESS;Database=Country_List;Integrated Security=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country_Image>()
                .HasOne(p => p.Country_Name)
                .WithMany(c => c.Country_Images)
                .HasForeignKey(p => p.CountryId);
        }
    }
}
