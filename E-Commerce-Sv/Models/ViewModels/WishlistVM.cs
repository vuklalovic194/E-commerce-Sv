namespace E_Commerce_Sv.Models.ViewModels
{
	public class WishlistVM
	{
		public IEnumerable<Wishlist> WishList { get; set; }
		public Product Products { get; set; }
	}
}
