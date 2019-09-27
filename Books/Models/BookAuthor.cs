using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
	public class BookAuthor
	{
		// placing the Book & Author in here let the EF what table we're connecting to
		public Guid BookId { get; set; }
		public Book Book { get; set; }
		public Guid AuthorId { get; set; }
		public Author Author { get; set; }
	}
}
