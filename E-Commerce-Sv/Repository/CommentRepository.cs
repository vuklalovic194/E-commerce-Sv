using E_Commerce_Sv.Data;
using E_Commerce_Sv.Models;
using E_Commerce_Sv.Repository.IRepository;
using System.ComponentModel;

namespace E_Commerce_Sv.Repository
{
	public class CommentRepository : Repository<Comment>, ICommentRepository
	{
		private readonly ApplicationDbContext _db;

        public CommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


		public void Update(Comment comment)
		{
			_db.Update(comment);
		}
	}
}
