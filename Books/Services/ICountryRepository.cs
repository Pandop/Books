using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Services
{
	public interface ICountryRepository
	{
		Task<Country> GetCountryAsync(Guid countryId);
		Task<Country> GetCountryOfAnAuthorAsync(Guid authorId);
		Task<IEnumerable<Country>> GetCountriesAsync();
		Task<IEnumerable<Author>> GetAuthorsFromACountryAsync(Guid countryId);
		Task<bool> CountryExistsAsync(Guid countryId);
		Task<bool> IsDuplicateCountryName(Guid countryId, string countryName);
	}
}
