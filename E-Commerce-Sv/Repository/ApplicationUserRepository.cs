using E_Commerce_Sv.Data;
using E_Commerce_Sv.Models;
using E_Commerce_Sv.Repository.IRepository;

namespace E_Commerce_Sv.Repository
{
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
	{
		private readonly ApplicationDbContext _db;
		public ApplicationUserRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}
