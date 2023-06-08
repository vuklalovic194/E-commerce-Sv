using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Sv.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Range(3, 50)]
		public string? Name { get; set; }
		[Required]
		public int Price { get; set; }
		[StringLength(50)]
		public string? Description { get; set; }
		public string? Category { get; set; }
		public int CategoryId { get; set; }
	}
}
