using UESAN.ECOMMERCE.CORE.Core.DTOs;
using UESAN.ECOMMERCE.CORE.Core.Entities;
using UESAN.ECOMMERCE.CORE.Core.Interfaces;

namespace UESAN.ECOMMERCE.CORE.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductListDTO>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            var productsDTO = new List<ProductListDTO>();

            foreach (var product in products)
            {
                var productDTO = new ProductListDTO()
                {
                    Id = product.Id,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Stock = product.Stock ?? 0,
                    Price = product.Price ?? 0,
                    Discount = product.Discount ?? 0,
                    Category = product.Category != null ? new CategoryListDTO
                    {
                        Id = product.Category.Id,
                        Description = product.Category.Description
                    } : null
                };

                productsDTO.Add(productDTO);
            }
            return productsDTO;
        }

        public async Task<ProductListDTO> GetProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null) return null;

            var productDTO = new ProductListDTO()
            {
                Id = product.Id,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Stock = product.Stock ?? 0,
                Price = product.Price ?? 0,
                Discount = product.Discount ?? 0,
                Category = product.Category != null ? new CategoryListDTO
                {
                    Id = product.Category.Id,
                    Description = product.Category.Description
                } : null
            };

            return productDTO;
        }

        public async Task<bool> CreateProduct(ProductCreateDTO productCreateDTO)
        {
            var product = new Product()
            {
                IsActive = true,
                Description = productCreateDTO.Description,
                ImageUrl = productCreateDTO.ImageUrl,
                Stock = productCreateDTO.Stock,
                Price = productCreateDTO.Price,
                Discount = productCreateDTO.Discount,
                CategoryId = productCreateDTO.CategoryId
            };

            return await _productRepository.CreateProduct(product);
        }

        public async Task<bool> UpdateProduct(ProductUpdateDTO productUpdateDTO)
        {
            var product = new Product()
            {
                Id = productUpdateDTO.Id,
                Description = productUpdateDTO.Description,
                ImageUrl = productUpdateDTO.ImageUrl,
                Stock = productUpdateDTO.Stock,
                Price = productUpdateDTO.Price,
                Discount = productUpdateDTO.Discount,
                CategoryId = productUpdateDTO.CategoryId
            };

            return await _productRepository.UpdateProduct(product);
        }

        public async Task<bool> DeleteProduct(ProductDeleteDTO productDeleteDTO)
        {
            return await _productRepository.DeleteProduct(productDeleteDTO.Id);
        }
    }
}
