using System;
using System.Collections.Generic;
using System.Text;
using UESAN.ECOMMERCE.CORE.Core.DTOs;
using UESAN.ECOMMERCE.CORE.Core.Entities;
using UESAN.ECOMMERCE.CORE.Core.Interfaces;

namespace UESAN.ECOMMERCE.CORE.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryListDTO>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            var categoriesDTO = new List<CategoryListDTO>();

            foreach (var category in categories)
            {
                var categoryDTO = new CategoryListDTO();
                categoryDTO.Id = category.Id;
                categoryDTO.Description = category.Description;

                categoriesDTO.Add(categoryDTO);
            }
            return categoriesDTO;
        }

        public async Task<CategoryListDTO> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            var categoryDTO = new CategoryListDTO();
            categoryDTO.Id = category.Id;
            categoryDTO.Description = category.Description;

            return categoryDTO;
        }

        public async Task<bool> CreateCategory(CategoryCreateDTO categoryCreateDTO)
        {
            var category = new Category();
            category.IsActive = true;
            category.Description = categoryCreateDTO.Description;

            return await _categoryRepository.CreateCategory(category);
        }

        public async Task<bool> UpdateCategory(CategoryUpdateDTO categoryUpdateDTO)
        {
            var category = new Category();
            category.Id = categoryUpdateDTO.Id;
            category.Description = categoryUpdateDTO.Description;

            return await _categoryRepository.UpdateCategory(category);
        }

        public async Task<bool> DeleteCategory(CategoryDeleteDTO categoryDeleteDTO)
        {
            return await _categoryRepository.DeleteCategory(categoryDeleteDTO.Id);
        }


    }
}
