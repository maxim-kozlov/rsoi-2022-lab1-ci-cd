using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Persons.Gateway.Database;
using Persons.Gateway.Extensions;

namespace Persons.Gateway;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        if (args.Length > 0 && args[1] == "--run-migration")
            await host.MigrateDatabaseAsync<PersonsContext>();
        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .SetupSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"));
            });
}