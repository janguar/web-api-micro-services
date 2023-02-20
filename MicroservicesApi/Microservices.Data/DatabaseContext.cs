using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Data;

public class DatabaseContext: DbContext, IDbContext
{
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    public IQueryable<T> GetData<T>(bool trackingChanges = false) where T : class
    {
        var set = Set<T>();
        return trackingChanges ? set.AsTracking() : set.AsNoTrackingWithIdentityResolution();
    }

    public void Insert<T>(T entity) where T : class => Set<T>().Add(entity);

    public void Delete<T>(T entity) where T : class => Set<T>().Remove(entity);

    public Task SaveAsync()
        => SaveChangesAsync();
}