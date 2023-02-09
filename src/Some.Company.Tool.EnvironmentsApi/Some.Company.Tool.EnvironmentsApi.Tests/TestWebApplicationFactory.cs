using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Some.Company.Tool.EnvironmentsApi.Tests;

internal class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<EnvironmentDb>)
            );

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<EnvironmentDb>(options =>
            {
                options.UseInMemoryDatabase("TESTS");
            });
        });

        return base.CreateHost(builder);
    }
}