using E_Commerce_Sv.Models;
using System.Linq.Expressions;

namespace E_Commerce_Sv.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category category);
		void Save();
	}
}
