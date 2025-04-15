using InventoryManagement.API.Database;
using InventoryManagement.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public PurchasesController(DatabaseContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> RecordPurchase(Purchase purchase)
        {
            try
            {
                var product = await _dbContext.Products.FindAsync(purchase.ProductId);
                if (product == null) return NotFound("Product not found!");
                product.Quantity += purchase.QuantityPurchased;
                _dbContext.Purchases.Add(purchase);
                await _dbContext.SaveChangesAsync();
                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
