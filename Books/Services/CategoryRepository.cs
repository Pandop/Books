using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private BookDbContext _categoryContext { get; set; }

        public CategoryRepository(BookDbContext categoryContext)
        {
            _categoryContext = categoryContext;
        }
        public Task<bool> CategoryExistsAsync(Guid categoryId)
        {
            // No categoryId is null or empty
            if(categoryId == Guid.Empty) throw new ArgumentNullException(nameof(categoryId));

            // Category Exists
            var categoryExistTask = _categoryContext.Categories.Any(c => c.Id == categoryId);

            // Return Task
            return Task.FromResult(categoryExistTask);
        }

        public Task<IEnumerable<Book>> GetAllBooksForCategoryAsync(Guid categoryId)
        {
            // No categoryId is null or empty
            if (categoryId == Guid.Empty)
                throw new ArgumentNullException(nameof(categoryId));

            // Valid bookId provided
            var booksTask = _categoryContext.BookCategories
                                              .Where(c => c.CategoryId == categoryId)
                                              .Select(b => b.Book)
                                              .AsEnumerable();
            // Return Task
            return Task.FromResult(booksTask);
        }

        public Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            // Get categories task
            var categoriesTask =  _categoryContext.Categories.OrderBy(c=>c.Name).AsEnumerable();

            // Return task
            return Task.FromResult(categoriesTask);
        }

        public Task<IEnumerable<Category>> GetAllCategoriesForABookAsync(Guid bookId)
        {
            // No categoryId is null or empty
            if (bookId == Guid.Empty)
                throw new ArgumentNullException(nameof(bookId));

            // Valid bookId provided
            var categories = _categoryContext.BookCategories
                                         .Where(b => b.BookId == bookId)
                                         .Select(c=> c.Category)
                                         .AsEnumerable();

            // Return task
            return Task.FromResult(categories);    
        }

        public Task<Category> GetCategoryAsync(Guid categoryId)
        {
            // No categoryId is null or empty
            if (categoryId == Guid.Empty)
                throw new ArgumentNullException(nameof(categoryId));
           
            // Get category Task
            var categoryTask = _categoryContext.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            // Return first if found otherwise null
            return Task.FromResult(categoryTask);
        }
    }
}
