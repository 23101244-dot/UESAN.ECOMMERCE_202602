using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Text;
using UESAN.ECOMMERCE.CORE.Core.Entities;
using UESAN.ECOMMERCE.CORE.Infrastructure.Data;

namespace UESAN.ECOMMERCE.CORE.Infrastructure.Repositories
{
    public class CategoryRepository
    {
        private readonly StoreDbContext _dbContext;

        public CategoryRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetCategories() 
        {
            var categories = await _dbContext.Category.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _dbContext
                                .Category
                                .Where(c => c.Id == id)
                                .FirstOrDefaultAsync();
            return category;
        }

        public async Task<bool> CreateCategory(Category category)
        {
            _dbContext.Category.AddAsync(category);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            var existingCategory = await _dbContext
                                .Category
                                .Where(c => c.Id == category.Id)
                                .FirstOrDefaultAsync();
            if (existingCategory != null) {
                existingCategory.Description = category.Description;
                var rows = await _dbContext.SaveChangesAsync();
                return rows > 0;
            }
            return false;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var existingCategory = await _dbContext
                               .Category
                               .Where(c => c.Id == id)
                               .FirstOrDefaultAsync();
            if (existingCategory != null)
            {
                //Eliminación lógica
                existingCategory.IsActive = false;
                var rows = await _dbContext.SaveChangesAsync();
                return rows > 0;
            }
            return false;
        }



    }
}
