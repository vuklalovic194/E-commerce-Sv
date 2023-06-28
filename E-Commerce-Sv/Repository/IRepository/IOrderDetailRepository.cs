using E_Commerce_Sv.Models;
using System.Linq.Expressions;

namespace E_Commerce_Sv.Repository.IRepository
{
	public interface IOrderDetailRepository : IRepository<OrderDetail>
	{
		void Update(OrderDetail obj);
	}
}
