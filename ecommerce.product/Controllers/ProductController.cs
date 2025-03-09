using ecommerce.product.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ProductDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<model.ProductModel>>> GetProducts()
        {
            return await dbContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    } 

}