using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using NetworkDatabase;
using DAL.Interfaces;

namespace DAL.Implementations
{
    internal class EntityRepository<T>: IRepository<T> where T: class
    {
        private NetworkContext _networkContext;
        protected DbSet<T> _dbSet;

        public EntityRepository(NetworkContext networkContext)
        {
            _networkContext = networkContext;
            _dbSet = networkContext.Set<T>();
        }

        public List<T> All
        {
            get
            {
                return _dbSet.Select(entity=>entity).ToList();
            }
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.AddOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}