using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDeliveryAPI.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "administrator, dostavljac, potrosac")]
        public IActionResult GetAllProducts()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "administrator, dostavljac, potrosac")]
        public IActionResult Get(long id)
        {
            return Ok(_productService.GetProductById(id));
        }

        [HttpPost]
        [Authorize(Roles = "administrator")]
        public IActionResult CreateProduct([FromBody] ProductDTO product)
        {
            return Ok(_productService.AddNewProduct(product));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "administrator")]
        public IActionResult UpdateProduct([FromBody] ProductDTO productDTO, long id)
        {
            return Ok(_productService.UpdateProduct(productDTO, id));
        }
    }
}
