using System.Diagnostics;
using Microservice.Data;
using Microservice.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestMicroservice.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IDbContext dbContext;

        public ProductsController(IDbContext dbContext)
        {
            this.dbContext = dbContext; 
        } 
            
        [HttpGet("/all")]
        public async Task<IEnumerable<Product>> GetAsync()
        {
            List<Product> people = new List<Product>();

            try
            {
                people = dbContext.GetData<Product>()
                    .Include(i => i.Category)
                    .Include(i => i.Supplier)
                    // .ThenInclude(s => s.Products)
                    .Include(i => i.OrderDetails)
                    // .ThenInclude(s => s.Product)
                    // .Include(i => i.OrderDetails)
                    // .ThenInclude(s => s.Order)
                    .Where(p => p.ProductID == 6)
                    .OrderBy(p => p.Category)  
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return people; 
        }
    }
}
