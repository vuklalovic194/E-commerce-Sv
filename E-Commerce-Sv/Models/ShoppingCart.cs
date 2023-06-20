namespace E_Commerce_Sv.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int Count { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
