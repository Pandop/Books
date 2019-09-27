using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
	public class Category
	{
		//[Key]
		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		//public int Id { get; set; }


		[Key]
		public Guid Id { get; set; }

		[Required]
		[MaxLength(50, ErrorMessage = "Category cannot be more than 50 characters")]
		public string Name { get; set; }
		public virtual ICollection<BookCategory> BookCategories { get; set; }

	}
}
