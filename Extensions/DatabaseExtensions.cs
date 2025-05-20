using DDDExample.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DDDExample.Extensions;

public static class DatabaseExtensions
{
    public static IHost ApplyDbMigrations(this IHost host)
    {
        using var serviceScope = host.Services.CreateScope();
        using var context = (DbContext)serviceScope.ServiceProvider.GetRequiredService<BankingDbContext>();

        context.Database.Migrate();

        return host;
    }
}
