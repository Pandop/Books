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
    public class ReviewersController: Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        public ReviewersController(IReviewerRepository reviewerRepository)
        {
            _reviewerRepository = reviewerRepository;
        }

        // api/reviewers
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public async Task<IActionResult> GetReviewersAsync()
        {
            // Get reviewers
            var reviewers = await _reviewerRepository.GetReviewersAsync();

            // If something went wrong while retrieving reviewers
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Otherwise map Reviewer to DTO
            var reviewersDto = new List<ReviewerDto>();
            foreach (var reviewer in reviewers)
            {
                reviewersDto.Add(new ReviewerDto()
                {
                    Id = reviewer.Id,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName                    
                });
            }

            // return reviewers
            return Ok(reviewersDto);
        }

        //api/reviewers/reviewerId
        [HttpGet("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(ReviewerDto))]
        public async  Task<IActionResult>GetReviewerAsync(Guid reviewerId)
        {
            // Reviewer does not exist
            if (!await _reviewerRepository.ReviewerExistsAsync(reviewerId))
                return NotFound();

            // Reviewer exists
            var reviewer = await _reviewerRepository.GetReviewerAsync(reviewerId);

            // Something wrong while retrieving reviewer
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Otherwise map Reviewer to DTO
            var reviewerDto = new ReviewerDto()
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };

            // Return reviewer Dto
            return Ok(reviewerDto);
        }
        
        //api/reviewers/reviewerId/reviews
        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public async Task<IActionResult> GetAllReviewsByReviewerAsync(Guid reviewerId)
        {
            // Reviewer does not exist
            if (!await _reviewerRepository.ReviewerExistsAsync(reviewerId))
                return NotFound();

            // Reviewer exists hence retrieve reviews
            var reviews = await _reviewerRepository.GetAllReviewsByReviewerAsync(reviewerId);

            // Something wrong while retrieving reviewer
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

            // Return reviews Dto
            return Ok(reviewsDto);
        }




    }
}
