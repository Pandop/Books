using Books.Dtos;
using Books.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Books.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountriesController : Controller
	{
		private readonly ICountryRepository _countryRepository;
		private readonly IAuthorRepository _authorRepository;
		public CountriesController(ICountryRepository countryRepository, IAuthorRepository authorRepository)
		{
			_countryRepository = countryRepository;
			_authorRepository = authorRepository;
		}

		//api/countries
		[HttpGet]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type =typeof(IEnumerable<CountryDto>))]
		public async Task <IActionResult> GetCountriesAsync()
		{
			var countries = await _countryRepository.GetCountriesAsync();

			// If Something went wrong
			if (!ModelState.IsValid)    return BadRequest(ModelState);


			// proceed papping countries to data transfer object
			var countriesDto = new List<CountryDto>();            
			foreach (var country in countries)
			{
				countriesDto.Add(new CountryDto{
					Id = country.Id,
					Name = country.Name,
				});
			}
			return Ok(countriesDto);
		}

		//api/countries/countryId
		[HttpGet("{countryId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(CountryDto))]
		public async Task<IActionResult> GetCountryAsync(Guid countryId)
		{
			// country does not exist
			if (!await _countryRepository.CountryExistsAsync(countryId))
				return NotFound();

			// Country does exist
			var country = await _countryRepository.GetCountryAsync(countryId);

			// If Something went wrong retrieving a country
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Mapping Country to DTO
			var countryDto = new CountryDto(){ Id = country.Id, Name = country.Name };

			// Return country
			return Ok(countryDto);
		}


		//api/countries/authors/authorId
		[HttpGet("authors/{authorId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(CountryDto))]
		public async Task<IActionResult> GetCountryOfAnAuthorAsync(Guid authorId)
<<<<<<< HEAD
		{            
=======
		{
>>>>>>> feature/review
			// Author does not exist
			if (!await _authorRepository.AuthorExistsAsync(authorId))
				return NotFound();

			var country = await _countryRepository.GetCountryOfAnAuthorAsync(authorId);

			// Something went wrong retrieving author's country
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			//TODO: Map Country Object to DTO
			return Ok();
		}

		//api/countries/countryId/authors
		[HttpGet("{countryId}/authors")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
		public async Task<IActionResult> GetAuthorsForCountryAsync(Guid countryId)
		{
			// country does not exist
			if (!await _countryRepository.CountryExistsAsync(countryId))
				return NotFound();

			// validate country exists
			var authors = await _countryRepository.GetAuthorsFromACountryAsync(countryId);

			// Something went wrong retrieving author's country
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Map Country Object to DTO
			var authorsDto = new List<AuthorDto>();
			foreach (var author in authors)
			{
				authorsDto.Add(new AuthorDto()
				{
					Id = author.Id,
					FirstName = author.FirstName,
					LastName = author.LastName

				});
			}
			return Ok(authorsDto);
		}
	}
}
