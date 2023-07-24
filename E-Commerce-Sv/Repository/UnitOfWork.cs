using E_Commerce_Sv.Data;
using E_Commerce_Sv.Repository.IRepository;

namespace E_Commerce_Sv.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _db;

		public ICategoryRepository CategoryRepository { get; private set; }
		public IProductRepository ProductRepository { get; private set; }
		public IShoppingCartRepository ShoppingCartRepository { get; private set; }
		public IApplicationUserRepository ApplicationUserRepository { get; private set; }
		public IOrderHeaderRepository OrderHeaderRepository { get; private set; }
		public IOrderDetailRepository OrderDetailRepository { get; private set; }
		public IWishlistRepository WishlistRepository { get; private set; }
		public ICommentRepository CommentRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
			CategoryRepository = new CategoryRepository(_db);
			ProductRepository = new ProductRepository(_db);
			ShoppingCartRepository = new ShoppingCartRepository(_db);
			ApplicationUserRepository = new ApplicationUserRepository(_db);
			OrderHeaderRepository = new OrderHeaderRepository(_db);
			OrderDetailRepository = new OrderDetailRepository(_db);
			WishlistRepository = new WishlistRepository(_db);
			CommentRepository = new CommentRepository(_db);

        }

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
