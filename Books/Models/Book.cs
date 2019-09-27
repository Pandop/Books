using System;

namespace Books.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string IsBN { get; set; }
		public DateTime? PublishedDateTime { get; set; }
		public bool Published { get; set; }
		// BookAuthors goes here
		// BookCategories goes here
		// Book Reviews goes here
	}
}
