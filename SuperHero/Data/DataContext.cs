using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace SuperHero.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server = BURAKPC\\BURAKPC; database = DbSuperHero; integrated security = true");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuperHero>().ToTable("SuperHeroes").HasKey(t => new {t.Id});
        }
        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}