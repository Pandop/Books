using Books.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Services
{
    public interface IReviewerRepository
    {
        Task<IEnumerable<Reviewer>> GetReviewersAsync();
        Task<Reviewer> GetReviewerAsync(Guid reviewerId);
        Task<IEnumerable<Review>> GetAllReviewsByReviewerAsync(Guid reviewerId);
        Task<Reviewer> GetReviewerForReviewAsync(Guid reviewId);
        Task<bool> ReviewerExistsAsync(Guid reviewerId);
    }
}
