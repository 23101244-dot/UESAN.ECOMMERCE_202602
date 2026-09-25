using UESAN.ECOMMERCE.CORE.Core.Entities;

namespace UESAN.ECOMMERCE.CORE.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> CreateCategory(Category category);
        Task<bool> DeleteCategory(int id);
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task<bool> UpdateCategory(Category category);
    }
}