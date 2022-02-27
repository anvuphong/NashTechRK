using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.DTO;
using Assignment2.Entities;
using Assignment2.Service;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _service;
        public CategoryController(ICategoryService service){
            _service = service;
        }

        [HttpPost("add-categgory")]
        public void CreateNewCategory(CategoryDTO categoryDTO)
        {
            _service.CreateNewCategory(categoryDTO);
        }
        [HttpDelete("delete-category")]
        public void DeleteCategory(int id)
        {
            _service.DeleteCategory(id);
        }
        [HttpPut("update-category")]
        public void UpdateCategory([FromBody] CategoryWithIdDTO categoryDTO)
        {
            _service.UpdateCategory(categoryDTO);
        }
        [HttpGet("get-all")]
        public List<Category> GetAllCategories()
        {
            return _service.GetAllCategories();
        }
        [HttpGet("get-one-category")]
        public Category GetCategoryById(int id)
        {
            return _service.GetCategoryById(id);
        }
    }
}