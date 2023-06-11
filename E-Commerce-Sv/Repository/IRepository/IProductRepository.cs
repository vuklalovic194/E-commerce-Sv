using E_Commerce_Sv.Models;

namespace E_Commerce_Sv.Repository.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{
		void Update(Product product);
		void Save();
	}
}
