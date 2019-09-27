namespace Books.Models
{
	public class Review
	{
		public int Id { get; set; }
		public string description { get; set; }
		public short Rating { get; set; }
		public Review Reviewer { get; set; }
		public int ReviewerId { get; set; }
		public Book Book { get; set; }
		public int BookId { get; set; }
	}
}
