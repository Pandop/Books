using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
	public class BookCategory
	{
		// placing the Book & Category in here let the EF what tables we're connecting
		public Guid BookId { get; set; }
		public Book Book { get; set; }
		public Guid CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
