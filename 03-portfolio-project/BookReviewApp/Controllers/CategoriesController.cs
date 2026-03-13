using BookReviewApp.DTO;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
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
                Name = c.Name
            }).ToList();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(categoriesDtos);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return BadRequest("Category not found.");
            var category = _categoryRepository.GetCategory(categoryId);
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(categoryDto);
        }


        [HttpGet("{categoryId}/books")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public IActionResult GetBooksByCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return BadRequest("Category not found.");
            var books = _categoryRepository.GetBooksByCategory(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(books);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null || categoryId != updatedCategory.Id)
                return BadRequest("Invalid category data.");
            if (!_categoryRepository.CategoryExists(categoryId))
                return BadRequest("Category not found.");
            var categoryToUpdate = _categoryRepository.GetCategory(categoryId);
            categoryToUpdate.Name = updatedCategory.Name;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _categoryRepository.UpdateCategory(categoryToUpdate);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto newCategory)
        {
            if (newCategory == null)
                return BadRequest("Invalid category data.");
            var existingCategory = _categoryRepository.GetCategories()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == newCategory.Name.Trim().ToUpper());
            if (existingCategory != null)
            {
                ModelState.AddModelError("", "Category already exists.");
                return StatusCode(422, ModelState);
            }
            var categoryToCreate = new Category
            {
                Name = newCategory.Name
            };

            if (!_categoryRepository.CreateCategory(categoryToCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving the category.");
                return StatusCode(500, ModelState);
            }
            return Ok("Category successfully created!");
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return BadRequest("Category not found.");
            var categoryToDelete = _categoryRepository.GetCategory(categoryId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting the category.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}