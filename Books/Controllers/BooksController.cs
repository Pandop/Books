using Books.Dtos;
using Books.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : Controller
	{
		private readonly IBookRepository _bookRepository;
		private readonly IAuthorRepository _authorRepository;
		public BooksController(IBookRepository bookRepository, IAuthorRepository authorRepository)
		{
			_bookRepository = bookRepository;
			_authorRepository = authorRepository;
		}

		//api/books
		[HttpGet]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
		public async Task<IActionResult> GetBooksAsync()
		{
			// Retrieve books from database
			var books = await _bookRepository.GetBooksAsync();

			// If something went wrong while fetching books
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Otherwise map Books to DTO
			var booksDto = new List<BookDto>();
			foreach (var book in books)
			{
				booksDto.Add(new BookDto
				{
					Id= book.Id,
					Title = book.Title,
					Isbn=book.Isbn,
					DatePublished = book.DatePublished
				});
			}

			return Ok(booksDto);
		}

		//api/books/bookId
		[HttpGet("{bookId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(BookDto))]
		public async Task<IActionResult> GetBookAsync(Guid bookId)
		{
			// Book does not exists
			if (!await _bookRepository.BookExistsAsync(bookId))
				return NotFound();

			// Fetch the book
			var book = await _bookRepository.GetBookAsync(bookId);

			// Something went wrong while fetching the book
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Otherwise map Book to DTO
			var bookDto = new BookDto
			{
				Id = book.Id,
				Title = book.Title,
				Isbn = book.Isbn,
				DatePublished = book.DatePublished
			};

			// Return book
			return Ok(bookDto);
		}

		//api/books/isbn/bookIsbn
		[HttpGet("ISBN/{bookIsbn}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(BookDto))]
		public async Task<IActionResult> GetBookAsync(string bookIsbn)
		{
			// Book does not exists
			if (!await _bookRepository.BookExistsAsync(bookIsbn))
				return NotFound();

			// Fetch the book
			var book = await _bookRepository.GetBookAsync(bookIsbn);

			// Something went wrong while fetching the book
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Otherwise map Book to DTO
			var bookDto = new BookDto
			{
				Id = book.Id,
				Title = book.Title,
				Isbn = book.Isbn,
				DatePublished = book.DatePublished
			};

			// Return book
			return Ok(bookDto);
		}

		//api/books/bookId/rating
		[HttpGet("{bookId}/rating")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(decimal))]
		public async Task<IActionResult> GetBookRatingAsync(Guid bookId)
		{
			// Book does not exists
			if (!await _bookRepository.BookExistsAsync(bookId))
				return NotFound();

			// Fetch the rating
			decimal rating = await _bookRepository.GetBookRatingAsync(bookId);

			// If something went wrong while fetching books
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Otherwise return rating
			return Ok(rating);
		}

	}
}
