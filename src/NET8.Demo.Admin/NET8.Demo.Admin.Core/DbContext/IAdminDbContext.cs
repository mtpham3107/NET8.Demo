using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NET8.Demo.Admin.Entities;

namespace NET8.Demo.Admin.Core.DbContext;

public interface IAdminDbContext : IDbContext
{
    public DbSet<User> Users { get; }
}
