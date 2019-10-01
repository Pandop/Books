using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;

namespace Books.Services
{
	public class ReviewRepository : IReviewRepository
	{
		private readonly BookDbContext _reviewContext;
		public ReviewRepository(BookDbContext reviewContext)
		{
			_reviewContext = reviewContext;
		}

		public Task<IEnumerable<Review>> GetAllReviewsForBookAsync(Guid bookId)
		{
			// No bookId is null or empty
			if (bookId == Guid.Empty) throw new ArgumentNullException(nameof(bookId));

			// Get reviews for given book in database
			return Task.FromResult( _reviewContext.Reviews.Where(b => b.Id == bookId).AsEnumerable());
			
		}

		public Task<Book> GetBookForReviewAsync(Guid reviewId)
		{
			// No categoryId is null or empty
			if (reviewId == Guid.Empty) throw new ArgumentNullException(nameof(reviewId));

			// Get book Id from reviews in database
			var bookId = _reviewContext
									.Reviews
									.Where(r => r.Id == reviewId)
									.Select(b => b.Book.Id)
									.FirstOrDefault();

			//  Return book of a review as task results
			return Task.FromResult(_reviewContext.Books.SingleOrDefault(b => b.Id == bookId));
		}

		public Task<Review> GetReviewAsync(Guid reviewId)
		{
			// No reviewId is null or empty
			if (reviewId == Guid.Empty) throw new ArgumentNullException(nameof(reviewId));

			// Return review as task results
			return Task.FromResult(_reviewContext.Reviews.Where(r => r.Id == reviewId).FirstOrDefault());
		}

		public Task<IEnumerable<Review>> GetReviewsAsync()
		{
			// Get reviews from database and return task results
			return Task.FromResult(_reviewContext.Reviews.OrderBy(r => r.Rating).AsEnumerable());
		}

		public Task<bool> ReviewExistsAsync(Guid reviewId)
		{
			// No reviewId is null or empty
			if (reviewId == Guid.Empty) throw new ArgumentNullException(nameof(reviewId));

			// Return true if review exists
			return Task.FromResult(_reviewContext.Reviews.Any(r => r.Id == reviewId));
		}
	}
}
