using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Sv.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Name must consider from 3 - 50 characters")]
		public string? Name { get; set; }
		[Required]
		public int Price { get; set; }
		[Required]
		[StringLength(500, MinimumLength = 3, ErrorMessage = "Name must consider from 3 - 500 characters")]
		public string? Description { get; set; }
		[ForeignKey("CategoryId")]
		[ValidateNever]
		public Category? Category { get; set; }
		public int CategoryId { get; set; }
		[ValidateNever]
		public string? ImageUrl { get; set; }

	}
}
