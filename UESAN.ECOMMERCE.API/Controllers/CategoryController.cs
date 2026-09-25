using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UESAN.ECOMMERCE.CORE.Core.DTOs;
using UESAN.ECOMMERCE.CORE.Core.Entities;
using UESAN.ECOMMERCE.CORE.Core.Interfaces;

namespace UESAN.ECOMMERCE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO category)
        {
            var result = await _categoryService.CreateCategory(category);
            if (!result) return BadRequest();
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateDTO category, int id)
        {
            if (id != category.Id)
                return BadRequest();

            var existingCategory = await _categoryService.GetCategoryById(id);
            if (existingCategory == null) return NotFound();

            var result = await _categoryService.UpdateCategory(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(CategoryDeleteDTO categoryDeleteDTO)
        {
            var existingCategory = await _categoryService.GetCategoryById(categoryDeleteDTO.Id);
            if (existingCategory == null) return NotFound();
            var result = await _categoryService.DeleteCategory(categoryDeleteDTO);
            return NoContent();
        }


    }
}
