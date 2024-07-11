using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NET8.Demo.Admin;
using NET8.Demo.Admin.Entities;
using NET8.Demo.Admin.Core.DbContext.Implements;
using NET8.Demo.Admin.Application.Services;
using IdentityServer4.AspNetIdentity;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("fr-FR")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

builder.Services.AddLocalization(options => options.ResourcesPath = "Localization");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = localizationOptions.DefaultRequestCulture;
    options.SupportedCultures = localizationOptions.SupportedCultures;
    options.SupportedUICultures = localizationOptions.SupportedUICultures;
});

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AdminDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.Password.RequiredLength = 9;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;

    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

}).AddEntityFrameworkStores<AdminDbContext>();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Account/Login";
    config.LogoutPath = "/Account/Logout";
});

var clientUrls = new Dictionary<string, string>
{
    { "App1", builder.Configuration["Identity:App1"] },
    { "App2", builder.Configuration["Identity:App2"] }
};

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(IdentityServerConfig.IdentityResources)
    .AddInMemoryApiResources(IdentityServerConfig.ApiResources)
    .AddInMemoryApiScopes(IdentityServerConfig.ApiScopes)
    .AddInMemoryClients(IdentityServerConfig.Clients(clientUrls))
    .AddAspNetIdentity<User>()
    .AddProfileService<ProfileService>()
    .AddDeveloperSigningCredential();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.ConfigureServices();
builder.Services.ConfigureEntityFrameworkCore();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
