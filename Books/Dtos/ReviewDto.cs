using System;

namespace Books.Dtos
{
	public class ReviewDto
	{
		public Guid Id { get; set; }
		public string Headline { get; set; }
		public string Comment { get; set; }
		public short Rating { get; set; }
	}
}
