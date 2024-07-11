using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NET8.Demo.Admin.Core.DbContext.Implements;

namespace C39Medical.ECO.Core;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class AdminDbContextFactory : IDesignTimeDbContextFactory<AdminDbContext>
{
    public AdminDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AdminDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"), n => n.MigrationsAssembly("NET8.Demo.Admin.Core"));

        return new AdminDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NET8.Demo.Admin/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
