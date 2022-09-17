using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Persons.Gateway.Extensions;

public static class MigrationExtensions
{
    public static async Task<IHost> MigrateDatabaseAsync<TContext>(this IHost webHost) where TContext : DbContext
    {
        await using var serviceScope = webHost.Services.CreateAsyncScope();
        await using var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();

        await context.Database.MigrateAsync();
        return webHost;
    }
}