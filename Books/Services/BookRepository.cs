using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;

namespace Books.Services
{
	public class BookRepository : IBookRepository
	{
		private readonly BookDbContext _bookDbContext;

		public BookRepository(BookDbContext bookDbContext)
		{
			_bookDbContext = bookDbContext;
		}
		public Task<bool> BookExistsAsync(Guid bookId)
		{
			if(bookId == Guid.Empty)
				throw new ArgumentNullException(nameof(bookId));

			// Book exists, return it task
			return Task.FromResult(_bookDbContext.Books.Any(b=> b.Id == bookId));
		}

		public Task<bool> BookExistsAsync(string bookIsbn)
		{
			if (string.IsNullOrEmpty(bookIsbn))
				throw new ArgumentNullException(nameof(bookIsbn));

			// Book exists, return it task
			return Task.FromResult(_bookDbContext.Books.Any(b => b.Isbn == bookIsbn));
		}

		public Task<Book> GetBookAsync(Guid bookId)
		{
			// Book Id is empty
			if (bookId == Guid.Empty)
				throw new ArgumentNullException(nameof(bookId));

			// Find book by bookId and return task
			return Task.FromResult(_bookDbContext.Books.Where(b => b.Id == bookId).FirstOrDefault());
		}

		public Task<Book> GetBookAsync(string bookIsbn)
		{
			// Book Isbn is empty or null
			if (string.IsNullOrEmpty(bookIsbn))
				throw new ArgumentNullException(nameof(bookIsbn));

			// Find book by bookIsbn and return task
			return Task.FromResult(_bookDbContext.Books.Where(b => b.Isbn == bookIsbn).FirstOrDefault());
		}

		public Task<decimal> GetBookRatingAsync(Guid bookId)
		{
			// Book Isbn is empty or null
			if (bookId == Guid.Empty)
				throw new ArgumentNullException(nameof(bookId));

			// Find book's rating from reviews 
			var reviewsTask = _bookDbContext
								.Reviews.Where(r => r.Book.Id == bookId)
								.Select(r => r.Rating)
								.Average(r => r);

			//and return average task
			return Task.FromResult(Convert.ToDecimal(reviewsTask));
		}

		public Task<IEnumerable<Book>> GetBooksAsync()
		{
			// Fetch all books and return as task
			return Task.FromResult(_bookDbContext.Books.OrderBy(b=>b.Title).AsEnumerable());
		}

		public Task<bool> IsDuplicateIsbnAsync(Guid bookId, string bookIsbn)
		{
			// Return true if book with similar bookId or Isbn exists in database
			return Task.FromResult(_bookDbContext.Books.Any(b=> b.Isbn.Trim().ToUpper()==bookIsbn.Trim().ToUpper() && b.Id != bookId));
		}
	}
}
