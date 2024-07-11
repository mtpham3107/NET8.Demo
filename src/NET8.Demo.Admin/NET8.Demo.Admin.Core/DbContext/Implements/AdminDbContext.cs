using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NET8.Demo.Admin.Entities;

namespace NET8.Demo.Admin.Core.DbContext.Implements;

public class AdminDbContext : IdentityDbContext<User>, IAdminDbContext
{
    public AdminDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<T> Repository<T>() where T : class
    {
        return Set<T>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        AddDefaultUser(builder);
    }

    private static void AddDefaultUser(ModelBuilder modelBuilder)
    {
        var hasher = new PasswordHasher<User>();

        var role = new List<IdentityRole>()
        {
            new ()
            {
                Id = "2c249336-6c74-40fb-be08-b9206981526e",
                Name = "Admin",
                NormalizedName = "Admin".ToUpper()
            },
        };

        var user = new User
        {
            Id = "708bc656-35fb-4863-89bb-03da44368a12",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "123456")
        };

        var userRole = new IdentityUserRole<string>
        {
            UserId = user.Id,
            RoleId = role[0].Id,
        };

        modelBuilder.Entity<IdentityRole>().HasData(role);
        modelBuilder.Entity<User>().HasData(user);
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRole);
    }
}
