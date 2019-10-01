using Books.Dtos;
using Books.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewsController:Controller
	{
		private readonly IReviewRepository _reviewRepository;
		private readonly IBookRepository _bookRepository;

		public ReviewsController(IBookRepository bookRepository, IReviewRepository reviewRepository)
		{
			_reviewRepository = reviewRepository;
			_bookRepository = bookRepository;
		}

		//api/reviews
		[HttpGet]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
		public async Task<IActionResult> GetReviewsAsync()
		{
			// Get reviews
			var reviews = await _reviewRepository.GetReviewsAsync();

			// If something went wrong while retrieving reviews
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Otherwise map Reviews to DTO
			var reviewsDto = new List<ReviewDto>();
			foreach (var review in reviews)
			{
				reviewsDto.Add(new ReviewDto()
				{
					Id = review.Id,
					Headline = review.Headline,
					Comment = review.Comment,
					Rating = review.Rating

				});
			}

			// return reviews
			return Ok(reviewsDto);
		}

		//api/reviews/reviewId
		[HttpGet("{reviewId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(ReviewDto))]
		public async Task<IActionResult> GetReviewAsync(Guid reviewId)
		{
			// Review does not exist
			if (!await _reviewRepository.ReviewExistsAsync(reviewId))
				return NotFound();

			// Review exists
			var review = await _reviewRepository.GetReviewAsync(reviewId);

			// Something wrong while retrieving reviewer
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Otherwise map Review to DTO
			var reviewDto = new ReviewDto()
			{
				Id = review.Id,
				Headline = review.Headline,
				Comment = review.Comment,
				Rating = review.Rating
			};

			// Return review Dto
			return Ok(reviewDto);
		}

		//api/reviews/reviewId/book
		[HttpGet("{reviewId}/book")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(BookDto))]
		public async Task<IActionResult> GetBookForReviewAsync(Guid reviewId)
		{
			// Review does not exist
			if (!await _reviewRepository.ReviewExistsAsync(reviewId))
				return NotFound();

			// Get Book from database
			var book = await _reviewRepository.GetBookForReviewAsync(reviewId);

			// Something wrong while retrieving reviewer
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Otherwise map Review to DTO
			var bookDto = new BookDto()
			{
				Id = book.Id,
				Title = book.Title,
				Isbn = book.Isbn,
				DatePublished = book.DatePublished
			};

			// Return book Dto
			return Ok(bookDto);
		}

		//api/reviews/books/bookId
		[HttpGet("books/{bookId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
		public async Task<IActionResult> GetAllReviewsForBookAsync(Guid bookId)
		{
			// Review does not exist
			if (!await _bookRepository.BookExistsAsync(bookId))
				return NotFound();

			// Get reviews of given book with given bookId from database
			var reviews = await _reviewRepository.GetAllReviewsForBookAsync(bookId);

			// Something wrong while retrieving reviewer
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Otherwise map Reviews to DTO
			var reviewsDto = new List<ReviewDto>();
			foreach (var review in reviews)
			{
				reviewsDto.Add( new ReviewDto
				{
					Id = review.Id,
					Headline = review.Headline,
					Comment = review.Comment,
					Rating = review.Rating
				});
			}

			// Return reviews Dto
			return Ok(reviewsDto);
		}
		

	}
}
