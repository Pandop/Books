namespace Books.Models
{
	public class Author
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Country Country { get; set; }
		public int CountryId { get; set; }
	}
}
