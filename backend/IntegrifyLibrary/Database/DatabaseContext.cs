using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using IntegrifyLibrary.Entities;
using Microsoft.Extensions.Configuration;

namespace IntegrifyLibrary.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var builder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Password = _configuration["DbPassword"]
            };
            optionsBuilder.UseNpgsql(builder.ConnectionString);
        }
    }
}