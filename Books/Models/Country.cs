using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
	public class Country
	{
		//[Key]
		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		//public int Id { get; set; }


		[Key]
		public Guid Id { get; set; }

		[Required]
		[MaxLength(50, ErrorMessage = "Country cannot be more than 50 characters")]
		public string Name { get; set; }

		// virtual to allow for overriding and lazy loading by EF
		public virtual IEnumerable<Author> Authors { get; set; }
	}
}
