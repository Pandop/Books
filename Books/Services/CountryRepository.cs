using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;

namespace Books.Services
{
	public class CountryRepository : ICountryRepository
	{
		private BookDbContext _countryContext { get; set; }
		public CountryRepository(BookDbContext countryContext)
		{
			_countryContext = countryContext;
		}

		public Task<IEnumerable<Author>> GetAuthorsFromACountryAsync(Guid countryId)
		{
			if (countryId == Guid.Empty)
				throw new ArgumentNullException(nameof(countryId));

			// Get Authors Task
			 var authorsTask = _countryContext.Authors.Where(c => c.Country.Id == countryId).AsEnumerable();

			// return Task
			return Task.FromResult(authorsTask);
		}

		public Task<Country> GetCountryAsync(Guid countryId)
		{
			if (countryId == Guid.Empty)
				throw new ArgumentNullException(nameof(countryId));

			// Fetch country and return as a task
			var countryTask = _countryContext.Countries.Where(c=>c.Id==countryId).FirstOrDefault();

			// Return Task
			return Task.FromResult(countryTask);
		}

		public Task<IEnumerable<Country>> GetCountriesAsync()
		{
			// Return countries as Task
			return Task.FromResult(_countryContext.Countries.OrderBy(c => c.Name).AsEnumerable());
		}

		public Task<Country> GetCountryOfAnAuthorAsync(Guid authorId)
		{
			if (authorId == Guid.Empty)
				throw new ArgumentNullException(nameof(authorId));

			// Fetch and return an author as Task
			var authorTask = _countryContext
									.Authors.Where(a => a.Id==authorId)
									.Select(c=> c.Country)
									.FirstOrDefault();

			// Return an author as task
			return Task.FromResult(authorTask);
		}

		public Task<bool> CountryExistsAsync(Guid countryId)
		{
			if (countryId == Guid.Empty)
				throw new ArgumentNullException(nameof(countryId));

			// return a task
			return Task.FromResult( _countryContext.Countries.Any(c=> c.Id==countryId));
		}

		public Task<bool> IsDuplicateCountryName(Guid countryId, string countryName)
		{
			// Return true if country with similar countryId or name exists in database
			return Task.FromResult(_countryContext.Countries.Any(c => c.Name.Trim().ToUpper() == countryName.Trim().ToUpper() && c.Id != countryId));
		}
	}
}
