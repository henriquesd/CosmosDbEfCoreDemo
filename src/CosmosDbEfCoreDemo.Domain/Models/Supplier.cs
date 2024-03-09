namespace CosmosDbEfCoreDemo.Domain.Models
{
    public class Supplier
    {
        public Guid SupplierId { get; set; }
        public Guid ProductId { get; set; }
        public required string Name { get; set; }
        public required string ContactEmail { get; set; }
    }
}