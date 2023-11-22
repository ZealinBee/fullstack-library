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
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

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
            .WithOne(book => book.Author);
        modelBuilder.Entity<User>()
            .HasMany(user => user.Loans)
            .WithOne(loan => loan.User);
        modelBuilder.Entity<Loan>()
            .HasMany(loan => loan.LoanDetails)
            .WithOne(loanDetail => loanDetail.Loan);
        modelBuilder.Entity<Genre>()
            .HasMany(genre => genre.Books)
            .WithOne(book => book.Genre);
        modelBuilder.Entity<Book>()
            .HasMany(book => book.LoanDetails)
            .WithOne(loanDetail => loanDetail.Book);
        modelBuilder.Entity<User>()
            .HasMany(user => user.Notifications)
            .WithOne(notification => notification.User);
        modelBuilder.Entity<User>()
            .HasMany(user => user.Reservations)
            .WithOne(reservation => reservation.User);
        modelBuilder.Entity<Book>()
            .HasMany(book => book.Reservations)
            .WithOne(reservation => reservation.Book);
    }
}