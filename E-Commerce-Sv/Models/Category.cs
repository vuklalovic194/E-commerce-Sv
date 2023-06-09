using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Sv.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		public string Description { get; set; }

	}
}
