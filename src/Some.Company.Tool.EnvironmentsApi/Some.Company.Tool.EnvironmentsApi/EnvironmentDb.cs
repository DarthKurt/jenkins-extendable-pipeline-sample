using Microsoft.EntityFrameworkCore;

namespace Some.Company.Tool.EnvironmentsApi;

internal sealed class EnvironmentDb : DbContext
{
    public EnvironmentDb(DbContextOptions<EnvironmentDb> options)
        : base(options) { }

    public DbSet<Environment> Environments => Set<Environment>();
}