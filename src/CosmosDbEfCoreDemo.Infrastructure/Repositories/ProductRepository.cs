using CosmosDbEfCoreDemo.Domain.Interfaces;
using CosmosDbEfCoreDemo.Domain.Models;
using CosmosDbEfCoreDemo.Infrastructure.Context;

namespace CosmosDbEfCoreDemo.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CosmosDbContext _dbContext;

        public ProductRepository(CosmosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Product product)
        {
            _dbContext.Add(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetById(Guid productId)
        {
            var product = await LoadProductWithReferences(productId);

            return product;
        }

        public async Task<Product?> Update(Product product)
        {
            // To update a Product and also update the related data in other containers,
            // it's necessary to also load the related data from the Inventory and Suppliers container.
            var existingProduct = await LoadProductWithReferences(product.ProductId);

            if (existingProduct == null) return null;

            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Dimensions = product.Dimensions;
            existingProduct.ShippingOptions = product.ShippingOptions;
            existingProduct.Suppliers = product.Suppliers;
            existingProduct.Inventory = product.Inventory;

            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<bool> Delete(Guid productId)
        {
            // To delete a Product and also delete the related data in other containers,
            // it's necesary to also load the related data from Inventory and Suppliers container.
            var product = await LoadProductWithReferences(productId);

            if (product == null) return false;

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<Product?> LoadProductWithReferences(Guid productId)
        {
            var product = await _dbContext
                .Products
                .FindAsync(productId);

            if (product == null) return null;

            var productEntry = _dbContext.Products.Entry(product);

            // Include the Inventory (which comes from another container)
            await productEntry
                .Reference(product => product.Inventory)
                .LoadAsync();

            // Include the Suppliers (which come from another container)
            await productEntry
                .Collection(product => product.Suppliers)
                .LoadAsync();

            return product;
        }
    }
}