using Microsoft.AspNetCore.Mvc;
using UESAN.ECOMMERCE.CORE.Core.DTOs;
using UESAN.ECOMMERCE.CORE.Core.Interfaces;

namespace UESAN.ECOMMERCE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO product)
        {
            var result = await _productService.CreateProduct(product);
            if (!result) return BadRequest();
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO product, int id)
        {
            if (id != product.Id)
                return BadRequest();

            var existingProduct = await _productService.GetProductById(id);
            if (existingProduct == null) return NotFound();

            var result = await _productService.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(ProductDeleteDTO productDeleteDTO)
        {
            var existingProduct = await _productService.GetProductById(productDeleteDTO.Id);
            if (existingProduct == null) return NotFound();
            var result = await _productService.DeleteProduct(productDeleteDTO);
            return NoContent();
        }
    }
}
