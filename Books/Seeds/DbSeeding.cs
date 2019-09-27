using Books.Models;
using Books.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Seeds
{
	public static class DbSeeding
	{
		public static void SeedDataContext(this BookDbContext bookContext)
		{
			// extension method hence this
			var bookAuthors = new List<BookAuthor>()
			{
				// new BookAuthor
			};
		}
	}
}
