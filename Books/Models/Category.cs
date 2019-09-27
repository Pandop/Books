using System.Collections;
using System.Collections.Generic;

namespace Books.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Book> Books { get; set; }

	}
}
