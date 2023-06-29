using E_Commerce_Sv.Data;
using E_Commerce_Sv.Models;
using E_Commerce_Sv.Repository.IRepository;
using System.ComponentModel;

namespace E_Commerce_Sv.Repository
{
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
		private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


		public void Update(OrderHeader obj)
		{
			_db.Update(obj);
		}

		public void UpdatePaymentIntentID(int id, string sessionId, string paymentIntentId)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u=>u.Id==id);
            if(!string.IsNullOrEmpty(sessionId))
			{
				orderFromDb.SessionId=sessionId;
				if (!string.IsNullOrEmpty(paymentIntentId))
				{
					orderFromDb.PaymentIntentId=paymentIntentId;
					orderFromDb.PaymentDate=DateTime.Now;
				}
			}
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u=>u.Id == id);
			if(orderFromDb != null)
			{
				orderFromDb.OrderStatus = orderStatus;
				if (!string.IsNullOrEmpty(paymentStatus))
				{
					orderFromDb.PaymentStatus = paymentStatus;
				}
			}
		}
	}
}
