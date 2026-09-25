using UESAN.ECOMMERCE.CORE.Core.DTOs;

namespace UESAN.ECOMMERCE.CORE.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(CategoryCreateDTO categoryCreateDTO);
        Task<bool> DeleteCategory(CategoryDeleteDTO categoryDeleteDTO);
        Task<IEnumerable<CategoryListDTO>> GetCategories();
        Task<CategoryListDTO> GetCategoryById(int id);
        Task<bool> UpdateCategory(CategoryUpdateDTO categoryUpdateDTO);
    }
}