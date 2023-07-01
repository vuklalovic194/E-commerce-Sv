using E_Commerce_Sv.Data;
using E_Commerce_Sv.Models;
using E_Commerce_Sv.Repository;
using E_Commerce_Sv.Repository.IRepository;

namespace E_Commerce_Sv
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
	{
        private readonly ApplicationDbContext _db;
       
        public WishlistRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

		public void Update(Wishlist wishList)
		{
			_db.Wishlists.Update(wishList);
		}
	}
}
