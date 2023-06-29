using E_Commerce_Sv.Models;
using System.Linq.Expressions;

namespace E_Commerce_Sv.Repository.IRepository
{
	public interface IOrderHeaderRepository : IRepository<OrderHeader>
	{
		void Update(OrderHeader obj);
		void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
		void UpdatePaymentIntentID(int id, string sessionId, string paymentIntentId);
	}
}
