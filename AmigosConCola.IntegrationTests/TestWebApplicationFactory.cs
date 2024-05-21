using AmigosConCola.WebApi.Data.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace AmigosConCola.IntegrationTests;

public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class
{
    private readonly PostgreSqlContainer _container;

    public TestWebApplicationFactory()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithPortBinding(5432, true)
            .WithResourceMapping("schema.sql", "/docker-entrypoint-initdb.d/")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(_container.GetConnectionString());
            });
        });
    }
}