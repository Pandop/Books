using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;

namespace Books.Services
{
	public class AuthorRepository : IAuthorRepository
	{
		public Task<bool> AuthorExistsAsync(Guid authorId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Author>> GetAllAuthorsForABookAsync(Guid bookId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Book>> GetAllBooksByAuthorAsync(Guid authorId)
		{
			throw new NotImplementedException();
		}

		public Task<Author> GetAuthorAsync(Guid authorId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Author>> GetAuthorsAsync()
		{
			throw new NotImplementedException();
		}
	}
}
