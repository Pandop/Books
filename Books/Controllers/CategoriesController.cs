using Books.Dtos;
using Books.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //api/categories
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            // If Something went wrong
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Mapping categories to Category DTO
            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoryDto() { Id = category.Id, Name = category.Name });
            }

            // Return category mapped to DTO
            return Ok(categoriesDto);
        }

        //api/categories/categoryId
        [HttpGet("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        public async Task<IActionResult> GetCategoryAsync(Guid categoryId)
        {
            // If category does not exist
            if (!await _categoryRepository.CategoryExistsAsync(categoryId))
                return NotFound();

            // Retrieve category if it exists 
            var category = await _categoryRepository.GetCategoryAsync(categoryId);

            // If something went wrong while retrieving category
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Otherwise Map Category to DTO
            var categoryDto = new CategoryDto() { Id = category.Id, Name = category.Name };

            // Return category
            return Ok(categoryDto);
        }

        //api/categories/books/bookId
        [HttpGet("books/{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public async Task<IActionResult> GetAllCategoriesForABookAsync(Guid bookId)
        {
            // If Book does not exist
            //if (!await _categoryRepository.BookExistsAsync(bookId))
                //return NotFound();

            // Retrieve categories if book exists 
            var categories = await _categoryRepository.GetAllCategoriesForABookAsync(bookId);

            // If something went wrong while retrieving category
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //  Do mapping to DTO
            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoryDto() { Id = category.Id, Name = category.Name });

            }

            // Return categories
            return Ok(categoriesDto);
        }

        //api/categories/categoryId/books
        [HttpGet("{categoryId}/books")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        public async Task<IActionResult> GetAllBooksForCategoryAsync(Guid categoryId)
        {
            // If category does not exist
            if (!await _categoryRepository.CategoryExistsAsync(categoryId))
                return NotFound();

            // Retrieve category if it exists 
            var books = await _categoryRepository.GetAllBooksForCategoryAsync(categoryId);

            // If something went wrong while retrieving books
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Otherwise Map Book to DTO
            var booksDto = new List<BookDto>();
            foreach (var book in books)
            {
                booksDto.Add(new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Isbn = book.Isbn,
                    DatePublished = book.DatePublished
                });
            }
            
            // Return book list
            return Ok(booksDto);
        }
    }
}
