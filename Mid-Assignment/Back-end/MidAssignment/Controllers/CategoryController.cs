using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MidAssignment.DTO;
using MidAssignment.Entities;
using MidAssignment.Interfaces;

namespace MidAssignment.Controllers
{
    [ApiController]
    [Route("api/")]
    public class CategoryController : ControllerBase
    {
        private readonly ILibraryServices<Category> _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ILibraryServices<Category> categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost("category")]
        public IActionResult AddCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null) return BadRequest(ModelState);
            var category = _mapper.Map<Category>(categoryDTO);
            if (ModelState.IsValid)
            {
                _categoryService.Add(category);
                return Ok(category);
            }
            return BadRequest();
        }

        [HttpGet("category")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryWithIdDTO>))]
        public IActionResult GetAllCategories()
        {
            var categories = _mapper.Map<List<CategoryWithIdDTO>>(_categoryService.GetAll());
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(categories);
        }

        [HttpGet("category/{id}")]
        [ProducesResponseType(200, Type = typeof(CategoryWithIdDTO))]
        public IActionResult GetCategoryById(int id)
        {
            var category = _mapper.Map<CategoryWithIdDTO>(_categoryService.GetById(id));
            if (!_categoryService.GetAll().Any(c => c.CategoryId == id)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(category);
        }

        [HttpPut("category")]
        public IActionResult UpdateCategory([FromBody]CategoryWithIdDTO categoryDTO)
        {
            if (categoryDTO == null) return BadRequest(ModelState);
            var category = _mapper.Map<Category>(categoryDTO);
            if (!_categoryService.GetAll().Any(c => c.CategoryId == category.CategoryId)) return NotFound();
            if (ModelState.IsValid)
            {
                _categoryService.Update(category);
                return Ok(category);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("Category")]
        [ProducesResponseType(200, Type = typeof(CategoryDTO))]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (!_categoryService.GetAll().Any(c => c.CategoryId == category.CategoryId)) return NotFound();
            if (ModelState.IsValid)
            {
                _categoryService.Remove(category);
            }
            return Ok(category);
        }

    }
}