namespace InventoryManagement.API.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantitySold { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Product? Product { get; set; }
    }

}
