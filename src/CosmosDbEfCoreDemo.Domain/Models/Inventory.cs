namespace CosmosDbEfCoreDemo.Domain.Models
{
    public class Inventory
    {
        public Guid InventoryId { get; set; }
        public Guid ProductId { get; set; }
        public int StockQuantity { get; set; }
        public DateTime LastRestocked { get; set; }
    }
}