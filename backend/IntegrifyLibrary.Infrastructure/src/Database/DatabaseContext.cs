using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<LoanDetails> LoanDetails { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public DatabaseContext(IConfiguration configuration, DbContextOptions options) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new NpgsqlConnectionStringBuilder(_configuration.GetConnectionString("DefaultConnection"));
        optionsBuilder.UseNpgsql(builder.ConnectionString).UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Role>();
        modelBuilder.Entity<Author>()
            .HasMany(author => author.Books)
            .WithOne(book => book.Author)
            .HasForeignKey(book => book.AuthorId);
        modelBuilder.Entity<User>()
            .HasMany(user => user.Loans)
            .WithOne(loan => loan.User)
            .HasForeignKey(loan => loan.UserId);
        modelBuilder.Entity<Loan>()
            .HasMany(loan => loan.LoanDetails)
            .WithOne(loanDetail => loanDetail.Loan)
            .HasForeignKey(loanDetail => loanDetail.LoanId);
        modelBuilder.Entity<Genre>()
            .HasMany(genre => genre.Books)
            .WithOne(book => book.Genre)
            .HasForeignKey(book => book.GenreId);
    }
}