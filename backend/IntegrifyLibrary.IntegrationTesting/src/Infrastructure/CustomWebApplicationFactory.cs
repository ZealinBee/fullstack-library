using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using Respawn;
using Npgsql;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>, IAsyncLifetime

{
    public HttpClient HttpClient { get; private set; } = default!;

    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<DatabaseContext>((options, context) =>
            {
                context.UseNpgsql("Host=localhost;Port=5432;Username=tester;Password=password;Database=integration_library").UseSnakeCaseNamingConvention();
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DatabaseContext>();
                db.Database.EnsureCreated();
            }
        });
    }

    public async Task InitializeAsync()
    {
        HttpClient = CreateClient();
        _dbConnection = new NpgsqlConnection("Host=localhost;Port=5432;Username=tester;Password=password;Database=integration_library");
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "public" },
        });

    }

    public new async Task DisposeAsync()
    {

    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }


}
