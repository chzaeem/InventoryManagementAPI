using InventoryManagement.API.Database;
using InventoryManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Checking CI/CD Setup edit
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public ProductController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                return Ok(await _databaseContext.Products.ToArrayAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _databaseContext.Products.FindAsync(id);
                return product == null ? NotFound() : Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                if (product != null)
                {
                    _databaseContext.Products.Add(product);
                    await _databaseContext.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            try
            {
                var existingProduct = await _databaseContext.Products.FindAsync(product.Id);
                if (existingProduct != null)
                    return BadRequest("Did'nt find the product with same Id");
                _databaseContext.Update(product);
                await _databaseContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            try
            {
                var product = await _databaseContext.Products.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
                if (product != null)
                {
                    _databaseContext.Remove(product);
                    await _databaseContext.SaveChangesAsync();

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
