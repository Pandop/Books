using Books.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Services
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsAsync();
        Task<Reviewer> GetReviewAsync(Guid reviewId);
        Task<IEnumerable<Review>> GetAllReviewsForBookAsync(Guid bookId);
        Task<Book> GetBookForReviewAsync(Guid reviewId);
        Task<bool> ReviewExistsAsync(Guid reviewId);
    }
}
