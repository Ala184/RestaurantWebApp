using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDeliveryAPI.DTO;
using Microsoft.AspNetCore.Cors;

namespace FoodDeliveryAPI.Controllers
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
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_productService.GetProductById(id));
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDTO product)
        {
            return Ok(_productService.AddNewProduct(product));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromBody] ProductDTO productDTO, long id)
        {
            return Ok(_productService.UpdateProduct(productDTO, id));
        }
    }
}
