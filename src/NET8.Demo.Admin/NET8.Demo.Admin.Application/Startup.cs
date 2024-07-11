using Microsoft.Extensions.DependencyInjection;

namespace NET8.Demo.Admin;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        //services.AddControllersWithViews()
        //    .AddNewtonsoftJson(options =>
        //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //);

        //services.AddScoped(typeof(IAppService<>), typeof(AppService<>));
        //services.AddScoped<IUserService, UserService>();
        
        //IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
        //services.AddSingleton(mapper);
        //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}