using Microsoft.EntityFrameworkCore;

namespace NET8.Demo.Template.Core.DbContext;

public interface IDbContext : IDisposable
{
    DbSet<T> Repository<T>() where T : class;
}
