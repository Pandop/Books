using Books.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Services
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(Guid categoryId);
        Task<IEnumerable<Category>> GetAllCategoriesForABookAsync(Guid bookId);
        Task<IEnumerable<Book>> GetAllBooksForCategoryAsync(Guid categoryId);
        Task<bool> CategoryExistsAsync(Guid categoryId);
    }
}
