using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Sv.Models
{
	public class Wishlist
	{
		public int Id { get; set; }

		public int ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		[ValidateNever]
		public Product Product { get; set; }

		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }
	}
}
