using IntegrifyLibrary.Domain;

using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace IntegrifyLibrary.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LoanDetails> LoanDetails { get; set; }

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var connBuilder = new NpgsqlConnectionStringBuilder(connectionString);

            optionsBuilder.UseNpgsql(connBuilder.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanDetails>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.LoanId });
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}