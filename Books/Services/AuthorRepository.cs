using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;

namespace Books.Services
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly BookDbContext _authorContext;
		public AuthorRepository(BookDbContext authorContext)
		{
			_authorContext = authorContext;
		}
		public Task<bool> AuthorExistsAsync(Guid authorId)
		{
			// authorId is null or empty
			if (authorId == Guid.Empty) throw new ArgumentNullException(nameof(authorId));

			// Return true if author with authorId exists
			return Task.FromResult(_authorContext.Authors.Any(a => a.Id == authorId));
		}

		public Task<IEnumerable<Author>> GetAllAuthorsForABookAsync(Guid bookId)
		{
			// bookId is null or empty
			if (bookId == Guid.Empty) throw new ArgumentNullException(nameof(bookId));

			// Fetch all authors for a given bookId
			var authors = _authorContext.BookAuthors.Where(b => b.BookId == bookId).Select(a => a.Author).AsEnumerable();

			// Return authors as task result
			return Task.FromResult(authors);
		}

		public Task<IEnumerable<Book>> GetAllBooksByAuthorAsync(Guid authorId)
		{
			// authorId is null or empty
			if (authorId == Guid.Empty) throw new ArgumentNullException(nameof(authorId));

			// Fetch all books by the author with given authorId
			var books = _authorContext.BookAuthors.Where(a => a.AuthorId == authorId).Select(b => b.Book).AsEnumerable();

			// Return books as task results
			return Task.FromResult(books);
		}

		public Task<Author> GetAuthorAsync(Guid authorId)
		{
			// authorId is null or empty
			if (authorId == Guid.Empty) throw new ArgumentNullException(nameof(authorId));

			// Fetch author and return a task result
			return Task.FromResult(_authorContext.Authors.Where(a => a.Id == authorId).FirstOrDefault());
		}

		public Task<IEnumerable<Author>> GetAuthorsAsync()
		{
			// Fetch authors from database and return task results
			return Task.FromResult(_authorContext.Authors.OrderBy(a=> a.LastName).AsEnumerable());
		}
	}
}
