using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.DTO;
using Assignment2.Entities;
using Assignment2.Service;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductService _service;
        public ProductController(IProductService service){
            _service = service;
        }
        [HttpPost("add-product")]
        public void CreateNewProduct(ProductDTO productDTO)
        {
            _service.CreateNewProduct(productDTO);
        }
        [HttpDelete("delete-product")]
        public void DeleteProduct(int id)
        {
            _service.DeleteProduct(id);
        }
        [HttpPut("update-product")]
        public void UpdateProduct([FromBody] ProductWithIdDTO productDTO)
        {
            _service.UpdateProduct(productDTO);
        }
        [HttpGet("get-all")]
        public List<Product> GetAllProducts()
        {
            return _service.GetAllProducts();
        }
        [HttpGet("get-one-product")]
        public Product GetProductById(int id)
        {
            return _service.GetProductById(id);
        }
    }
}