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
		private readonly IReviewerRepository _reviewerRepository;
		private readonly IReviewRepository _reviewRepository;

		public ReviewsController(IReviewerRepository reviewerRepository, IReviewRepository reviewRepository)
		{
			_reviewRepository = reviewRepository;
			_reviewerRepository = reviewerRepository;
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


	}
}
