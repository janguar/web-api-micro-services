using System.Linq;
using System.Threading.Tasks;
using Microservice.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Data;

public class DatabaseContext: DbContext, IDbContext
{
    
    /// <summary>
    /// Gets or sets the categories.
    /// </summary>
    public DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Gets or sets the customer demographics.
    /// </summary>
    public DbSet<CustomerDemographic> CustomerDemographics { get; set; }

    /// <summary>
    /// Gets or sets the customers.
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Gets or sets the demographics.
    /// </summary>
    public DbSet<Demographic> Demographics { get; set; }

    /// <summary>
    /// Gets or sets the employees.
    /// </summary>
    public DbSet<Employee> Employees { get; set; }

    /// <summary>
    /// Gets or sets the employee territories.
    /// </summary>
    public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

    /// <summary>
    /// Gets or sets the order details.
    /// </summary>
    public DbSet<OrderDetail> OrderDetails { get; set; }

    /// <summary>
    /// Gets or sets the orders.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Gets or sets the products.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Gets or sets the regions.
    /// </summary>
    public DbSet<Region> Regions { get; set; }

    /// <summary>
    /// Gets or sets the shippers.
    /// </summary>
    public DbSet<Shipper> Shippers { get; set; }

    /// <summary>
    /// Gets or sets the suppliers.
    /// </summary>
    public DbSet<Supplier> Suppliers { get; set; }

    /// <summary>
    /// Gets or sets the territories.
    /// </summary>
    public DbSet<Territory> Territories { get; set; }
    
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerDemographic>().HasKey(m => new { m.CustomerID , m.DemographicID });
        // The entity type 'EmployeeTerritory' has multiple properties with the [Key] attribute. Composite primary keys configured
        // by placing the [PrimaryKey] attribute on the entity type class, or by using 'HasKey' in 'OnModelCreating'.
        modelBuilder.Entity<EmployeeTerritory>().HasKey(m => new { m.TerritoryID , m.EmployeeID });
        //  The entity type 'OrderDetail' has multiple properties with the [Key] attribute. Composite primary keys configured by placing the [PrimaryKey] attribute on the entity type class, or by using 'HasKey' in 'OnModelCreating'.
        modelBuilder.Entity<OrderDetail>().HasKey(m => new { m.ProductID , m.OrderID });
        base.OnModelCreating(modelBuilder);
    }
}