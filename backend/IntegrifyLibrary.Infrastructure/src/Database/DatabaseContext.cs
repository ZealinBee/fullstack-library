using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<User> Users { get; set; }

    public DatabaseContext(IConfiguration configuration, DbContextOptions options) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new NpgsqlConnectionStringBuilder(_configuration.GetConnectionString("DefaultConnection"));
        optionsBuilder.UseNpgsql(builder.ConnectionString).UseSnakeCaseNamingConvention();
    }
}