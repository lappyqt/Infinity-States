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
            optionsBuilder.UseNpgsql("Host = ec2-34-253-119-24.eu-west-1.compute.amazonaws.com; Port = 5432; Database = d90enbc0049crg; Username = qrbmtayikihwdr; Password = e2af8ad06fd8d5dc6c40e13ff9223265507594bff930879bfffc861286a13fd4;");
        }
    }
}
