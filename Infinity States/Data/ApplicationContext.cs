using System;
using System.Threading.Tasks;
using Infinity_States.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infinity_States.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }

        public ApplicationContext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasGeneratedTsVectorColumn(
                p => p.SearchVector,
                "english", 
                p => new { p.Title, p.Content }).HasIndex(p => p.SearchVector).HasMethod("GIN");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Database = ISData1; Username = postgres; Password = adfQ312;");
        }
    }
}
