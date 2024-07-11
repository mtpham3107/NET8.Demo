using Microsoft.Extensions.DependencyInjection;
using NET8.Demo.Template.Core.DbContext;
using NET8.Demo.Template.Core.DbContext.Implements;

namespace NET8.Demo.Template;

public static class Startup
{
    public static void ConfigureEntityFrameworkCore(this IServiceCollection services)
    {
        services.AddScoped<IAdminDbContext, AdminDbContext>();
        //services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
    }
}
