using BookReviewApp.DTO;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using BookReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            var categoriesDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categoriesDtos);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
                return BadRequest("Category not found");

             var category = _categoryRepository.GetCategory(id);
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };

             if(!ModelState.IsValid)
                return BadRequest(ModelState);
             return Ok(categoryDto);  
        }

        [HttpGet("{categoryId}/books")]
        [ProducesResponseType(200, Type = typeof(Book))]
        [ProducesResponseType(400)]

        public IActionResult GetBooksByCategory(int categoryId)
        {
            if (_categoryRepository.CategoryExists(categoryId))
                return BadRequest("Category not found");
            var books = _categoryRepository.GetBooksByCategory(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(books);
        }
    }
}
