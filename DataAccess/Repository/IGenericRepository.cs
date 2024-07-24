using System;
namespace NoteApp.DataAccess.Context
{
	public interface IGenericRepository<T> 
	{
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}

