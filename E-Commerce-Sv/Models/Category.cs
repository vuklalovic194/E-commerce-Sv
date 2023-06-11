using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Sv.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Must consider from 3 - 50 characters")]
		public string Name { get; set; }
		[Required]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Must consider from 5 - 50 characters")]
		public string Description { get; set; }

	}
}
