using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
	public class Review
	{
		//[Key]
		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		//public int Id { get; set; }

		[Key]
		public Guid Id { get; set; }

		[Required]
		[StringLength(200, MinimumLength = 10, ErrorMessage = "Comment must be between 10 and 200 characters")]
		public string Headline { get; set; }

		[Required]
		[StringLength(2000, MinimumLength = 50, ErrorMessage = "Comment must be between 50 and 2000 characters")]
		public string Comment { get; set; }

		[Required]
		[MaxLength(50, ErrorMessage = "Name cannot be more than 50 characters")]
		public short Rating { get; set; }

		// virtual to allow for overriding and lazy loading by EF
		public Review Reviewer { get; set; }
		public Book Book { get; set; }
	}
}
