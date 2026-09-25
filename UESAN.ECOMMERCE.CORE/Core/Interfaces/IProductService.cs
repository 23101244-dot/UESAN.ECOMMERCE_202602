using UESAN.ECOMMERCE.CORE.Core.DTOs;

namespace UESAN.ECOMMERCE.CORE.Core.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProduct(ProductCreateDTO productCreateDTO);
        Task<bool> DeleteProduct(ProductDeleteDTO productDeleteDTO);
        Task<IEnumerable<ProductListDTO>> GetProducts();
        Task<ProductListDTO> GetProductById(int id);
        Task<bool> UpdateProduct(ProductUpdateDTO productUpdateDTO);
    }
}
