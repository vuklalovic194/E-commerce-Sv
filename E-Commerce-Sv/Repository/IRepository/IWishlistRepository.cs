using E_Commerce_Sv.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace E_Commerce_Sv.Repository.IRepository
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        void Update(Wishlist wishList);
    }
}
