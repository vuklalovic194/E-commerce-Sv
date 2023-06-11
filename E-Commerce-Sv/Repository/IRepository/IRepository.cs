using System.Linq.Expressions;

namespace E_Commerce_Sv.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll();
		T Get(Expression<Func<T, bool>> filter);
		void RemoveRange(IEnumerable<T> entities);
		void Remove(T entity);
		void Add(T entity);
	}
}
