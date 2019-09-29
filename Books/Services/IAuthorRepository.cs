using Books.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Services
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorAsync(Guid authorId);
        Task<IEnumerable<Author>> GetAllAuthorsForABookAsync(Guid bookId);
        Task<IEnumerable<Book>> GetAllBooksByAuthorAsync(Guid authorId);
        Task<bool> AuthorExistsAsync(Guid authorId);
    }
}
