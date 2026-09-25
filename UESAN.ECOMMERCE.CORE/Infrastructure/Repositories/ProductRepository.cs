using Microsoft.EntityFrameworkCore;
using UESAN.ECOMMERCE.CORE.Core.Entities;
using UESAN.ECOMMERCE.CORE.Core.Interfaces;
using UESAN.ECOMMERCE.CORE.Infrastructure.Data;

namespace UESAN.ECOMMERCE.CORE.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _dbContext;

        public ProductRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _dbContext.Product
                .Where(p => p.IsActive == true)
                .Include(p => p.Category)
                .ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _dbContext
                                .Product
                                .Include(p => p.Category)
                                .Where(p => p.Id == id)
                                .FirstOrDefaultAsync();
            return product;
        }

        public async Task<bool> CreateProduct(Product product)
        {
            product.IsActive = true;
            await _dbContext.Product.AddAsync(product);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var existingProduct = await _dbContext
                                .Product
                                .Where(p => p.Id == product.Id)
                                .FirstOrDefaultAsync();
            if (existingProduct != null)
            {
                existingProduct.Description = product.Description;
                existingProduct.ImageUrl = product.ImageUrl;
                existingProduct.Stock = product.Stock;
                existingProduct.Price = product.Price;
                existingProduct.Discount = product.Discount;
                existingProduct.CategoryId = product.CategoryId;
                var rows = await _dbContext.SaveChangesAsync();
                return rows > 0;
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var existingProduct = await _dbContext
                               .Product
                               .Where(p => p.Id == id)
                               .FirstOrDefaultAsync();
            if (existingProduct != null)
            {
                // Eliminación lógica
                existingProduct.IsActive = false;
                var rows = await _dbContext.SaveChangesAsync();
                return rows > 0;
            }
            return false;
        }
    }
}
