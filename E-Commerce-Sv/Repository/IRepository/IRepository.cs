using System.Linq.Expressions;

namespace E_Commerce_Sv.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties= null);
		T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
		void RemoveRange(IEnumerable<T> entities);
		void Remove(T entity);
		void Add(T entity);
	}
}
