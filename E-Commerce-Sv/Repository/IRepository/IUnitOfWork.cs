﻿namespace E_Commerce_Sv.Repository.IRepository
{
	public interface IUnitOfWork 
	{
		ICategoryRepository CategoryRepository { get; }
		IProductRepository ProductRepository { get; }
		IShoppingCartRepository ShoppingCartRepository { get; }
		IApplicationUserRepository ApplicationUserRepository { get; }

		void Save();
	}
}
