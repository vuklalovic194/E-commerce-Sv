using E_Commerce_Sv.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace E_Commerce_Sv.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart shoppingCart);
    }
}
