using InventoryManagement.API.Database;
using InventoryManagement.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public SalesController(DatabaseContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> RecordSale(Sale sale)
        {
            try
            {

                var product = await _dbContext.Products.FindAsync(sale.ProductId);
                if (product == null || product.Quantity < sale.QuantitySold)
                {
                    return BadRequest("Product stock is not available!");
                }

                product.Quantity -= sale.QuantitySold;
                _dbContext.Sales.Add(sale);
                await _dbContext.SaveChangesAsync();
                return Ok(sale);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
    }

}
