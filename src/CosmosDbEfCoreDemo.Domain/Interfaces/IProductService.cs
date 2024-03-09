using CosmosDbEfCoreDemo.Domain.Models;

namespace CosmosDbEfCoreDemo.Domain.Interfaces
{
    public interface IProductService
    {
        Task<Product> Add(Product product);
        Task<Product?> GetById(Guid productId);
        Task<Product?> Update(Product product);
        Task<bool> Delete(Guid productId);
    }
}