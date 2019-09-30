using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;

namespace Books.Services
{
	public class ReviewerRepository : IReviewerRepository
	{
		private readonly BookDbContext _reviewerContext;
		public ReviewerRepository(BookDbContext reviewerContext)
		{
			_reviewerContext = reviewerContext;
		}

		public Task<IEnumerable<Review>> GetAllReviewsByReviewerAsync(Guid reviewerId)
		{
			// No categoryId is null or empty
			if (reviewerId == Guid.Empty) throw new ArgumentNullException(nameof(reviewerId));

			// Get reviews from database
			var reviewsTask = _reviewerContext.Reviews
											   .Where(r => r.Reviewer.Id == reviewerId)
											   .AsEnumerable();

			// Return reviews as Task
			return Task.FromResult(reviewsTask);
		}

		public Task<Reviewer> GetReviewerAsync(Guid reviewerId)
		{
			// No categoryId is null or empty
			if (reviewerId == Guid.Empty) throw new ArgumentNullException(nameof(reviewerId));

			// Get reviewer as Task
			var reviewerTask = _reviewerContext.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();

			// Return task
			return Task.FromResult(reviewerTask);
		}

		public Task<Reviewer> GetReviewerForReviewAsync(Guid reviewId)
		{
			// No categoryId is null or empty
			if (reviewId == Guid.Empty) throw new ArgumentNullException(nameof(reviewId));

			// Get reviewer Id from reviews in database
			var reviewerId = _reviewerContext
									.Reviews
									.Where(r => r.Id == reviewId)
									.Select(r=> r.Reviewer.Id)
									.FirstOrDefault();
			// Get reviewer
			var reviewerTask = _reviewerContext.Reviewers.SingleOrDefault(r => r.Id == reviewerId);

			//  Return reviewer of a review
			return Task.FromResult(reviewerTask);
		}

		public Task<IEnumerable<Reviewer>> GetReviewersAsync()
		{
			// Get reviewers from database
			var reviewersTask = _reviewerContext.Reviewers
									.OrderBy(r=>r.LastName)
									.AsEnumerable();

			// Return reviewers as task
			return Task.FromResult(reviewersTask);
		}

		public Task<bool> ReviewerExistsAsync(Guid reviewerId)
		{
			// No categoryId is null or empty
			if (reviewerId == Guid.Empty) throw new ArgumentNullException(nameof(reviewerId));

			// Return reviewer as Task
			return Task.FromResult(_reviewerContext.Reviewers.Any(r => r.Id == reviewerId));
		}
	}
}
