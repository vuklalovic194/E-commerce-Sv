using E_Commerce_Sv.Models;

namespace E_Commerce_Sv.Repository.IRepository
{
	public interface ICommentRepository : IRepository<Comment>
	{
		void Update(Comment comment);
	}
}
