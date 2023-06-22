using E_Commerce_Sv.Data;
using E_Commerce_Sv.Models;
using E_Commerce_Sv.Repository;
using E_Commerce_Sv.Repository.IRepository;

namespace E_Commerce_Sv
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
       
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _db.ShoppingCarts.Update(shoppingCart);
        }
    }
}
