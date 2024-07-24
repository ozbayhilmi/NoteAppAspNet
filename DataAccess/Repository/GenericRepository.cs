using System;
using Microsoft.EntityFrameworkCore;

namespace NoteApp.DataAccess.Context
{
	public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class
                                                                                    where TContext : DbContext
	{
        protected TContext Context;

        public GenericRepository(TContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}



//db nedir
// db uygulamaları nelerdir /db nin kod içerisindeki uyarlanışı/kullanıcışı
// ado.net nedir napar
// entity framework nedir ne iş yapar neyi kolaylaştırır
// entitiy framework with .net core