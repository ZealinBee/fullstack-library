public class CustomWebApplicationFactory : WebApplicationFactory<Program>

{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDatabaseContext");
                });
        });
    }
}