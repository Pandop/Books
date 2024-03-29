﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
	public class Book
	{
		//[Key]
		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		//public int Id { get; set; }

		[Key]
		public Guid Id { get; set; }		

		[Required]
		[MaxLength(255, ErrorMessage = "Title cannot be more than 255 characters")]
		public string Title { get; set; }

		[Required]
		[StringLength(10,MinimumLength =3, ErrorMessage = "Isbn must be between 3 and 10 characters")]
		public string Isbn { get; set; }

		public DateTime? DatePublished { get; set; }

		// virtual to allow for overriding and lazy loading by EF		
		public virtual IEnumerable<Review> Reviews { get; set; }
		public virtual IEnumerable<BookAuthor> BookAuthors { get; set; }
		public virtual IEnumerable<BookCategory> BookCategories { get; set; }

		// [EntityForeignKey("Reviews", "Book", typeof(Book))]
		// [EntityForeignKey("BookAuthors", "Book", typeof(Book))]
		// [EntityForeignKey("BookCategories", "Book", typeof(Book))]
	}
}
