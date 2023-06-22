namespace E_Commerce_Sv.Models.ViewModels
{
	public class ShoppingCartVM
	{
		public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
		public double OrderTotal { get; set; }
	}
}
