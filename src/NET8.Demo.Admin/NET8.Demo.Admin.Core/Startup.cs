using Microsoft.Extensions.DependencyInjection;
using NET8.Demo.Admin.Core.DbContext;
using NET8.Demo.Admin.Core.DbContext.Implements;

namespace NET8.Demo.Admin;

public static class Startup
{
    public static void ConfigureEntityFrameworkCore(this IServiceCollection services)
    {
        services.AddScoped<IAdminDbContext, AdminDbContext>();
        //services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
    }
}
