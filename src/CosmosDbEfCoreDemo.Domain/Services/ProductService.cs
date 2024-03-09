using CosmosDbEfCoreDemo.Domain.Interfaces;
using CosmosDbEfCoreDemo.Domain.Models;

namespace CosmosDbEfCoreDemo.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> GetById(Guid productId)
        {
            var product = await _productRepository.GetById(productId);

            return product;
        }

        public async Task<Product> Add(Product product)
        {
            await _productRepository.Add(product);

            return product;
        }

        public async Task<Product?> Update(Product product)
        {
            var result = await _productRepository.Update(product);

            return result;
        }

        public async Task<bool> Delete(Guid productId)
        {
            var result = await _productRepository.Delete(productId);

            return result;
        }
    }
}