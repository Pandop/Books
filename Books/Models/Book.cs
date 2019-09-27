using System;
using System.Collections.Generic;

namespace Books.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string IsBN { get; set; }
		public DateTime? PublishedDateTime { get; set; }
		public ICollection<Author> Authors { get; set; }
		public ICollection<BookCategory> BookCategories { get; set; }
		public ICollection<Review> Reviews { get; set; }		
	}
}
