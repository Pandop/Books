using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
	public class Reviewer
	{
		//[Key]
		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		//public int Id { get; set; }


		[Key]
		public Guid Id { get; set; }

		[Required]
		[MaxLength(100, ErrorMessage = "First Name cannot be more than 100 characters")]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(150, ErrorMessage = "Last Name cannot be more than 150 characters")]
		public string LastName { get; set; }
		public virtual IEnumerable<Review> Reviews{ get; set; }
	}
}
