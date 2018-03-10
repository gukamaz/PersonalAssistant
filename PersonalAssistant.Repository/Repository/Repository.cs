using PersonalAssistant.DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(t => !t.IsDeleted).FirstOrDefault(predicate);
        }

        public TEntity Get(int ID)
        {
            return Context.Set<TEntity>().Find(ID);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().Where(t => !t.IsDeleted);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Context.Set<TEntity>().AsEnumerable().GetEnumerator();
        }

        public IQueryable<TEntity> GetRange(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(t => !t.IsDeleted).Where(predicate);
        }

        public void Remove(int entityID)
        {
            var entity = Get(entityID);
            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
        }

        public void Remove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
        }

        Expression IQueryable.Expression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        Type IQueryable.ElementType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
