namespace Books.Models
{
	public class Review
	{
		public int Id { get; set; }
		public string description { get; set; }
		public short Rating { get; set; }
		// Review's Reviewer goes here
		// Review's Book goes here
	}
}
