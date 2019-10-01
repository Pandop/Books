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
	public class AuthorsController:Controller
	{
		private readonly IAuthorRepository _authorRepository;

		public AuthorsController(IAuthorRepository authorRepository)
		{
			_authorRepository = authorRepository;
		}

		//api/authors
		[HttpGet]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
		public async Task<IActionResult> GetBooksAsync()
		{
			// Retrieve authors from database
			var authors = await _authorRepository.GetAuthorsAsync();

			// If something went wrong while fetching authors
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Otherwise map Books to DTO
			var authorsDto = new List<AuthorDto>();
			foreach (var author in authors)
			{
				authorsDto.Add(new AuthorDto
				{
					Id = author.Id,
					FirstName=author.FirstName,
					LastName=author.LastName
				});
			}

			return Ok(authorsDto);
		}
	}
}
