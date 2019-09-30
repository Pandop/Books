using Books.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Services
{
	public interface IBookRepository
	{
		Task<IEnumerable<Book>> GetBooksAsync();
		Task<Book> GetBookAsync(Guid bookId);
		Task<Book> GetBookAsync(string bookIsbn);
		Task<decimal> GetBookRatingAsync(Guid bookId);
		Task<bool> BookExistsAsync(Guid bookId);
		Task<bool> IsDuplicateIsbnAsync(Guid bookId, string bookIsbn);
	}
}
