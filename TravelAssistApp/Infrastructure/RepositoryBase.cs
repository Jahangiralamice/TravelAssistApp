using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace TravelAssistApp.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private DbContext _entities;
        private readonly IDbSet<T> _dbset;
        public RepositoryBase(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
        }

        public virtual T GetById(long id)
        {
            return _dbset.Find(id);
        }

        public virtual T GetById(string id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }


        //public virtual IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order)
        //{
        //    var results = _dbset.OrderBy(order).Where(where).GetPage(page).ToList();
        //    var total = _dbset.Count(where);
        //    return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        //}

        //public virtual IPagedList<T> GetPagedDescending<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> orderByDescending)
        //{
        //    var results = _dbset.OrderByDescending(orderByDescending).Where(where).GetPage(page).ToList();
        //    var total = _dbset.Count(where);
        //    return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        //}

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault<T>();
        }

        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return _entities.Database.SqlQuery<T>(query, parameters).ToList();
        }

        public IEnumerable<TU> GetBy<TU>(Expression<Func<T, bool>> exp, Expression<Func<T, TU>> columns)
        {
            return _dbset.Where(exp).Select(columns);
        }

        public int GetCount(Expression<Func<T, bool>> where)
        {
            return _dbset.Count(where);
        }
        public bool IsExist(Expression<Func<T, bool>> predicate)
        {
            var count = _dbset.Count(predicate);
            return count > 0;
        }

        public void AddMultiple(IEnumerable<T> list)
        {
            _entities.Set<T>().AddRange(list);
        }

        public void DeleteMultiple(IEnumerable<T> list)
        {
            _entities.Set<T>().RemoveRange(list);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return _entities.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        //public IEnumerable<T> ExecStoreProcedure<T>(string sql, params object[] parameters)
        //{
        //    return _entities.Database.SqlQuery<T>(sql, parameters);
        //}

        //public IEnumerable<T> SQLQueryList<T>(string sql)
        //{
        //    return _entities.Database.SqlQuery<T>(sql);
        //}

        //public T SQLQuery<T>(string sql)
        //{
        //    return _entities.Database.SqlQuery<T>(sql).FirstOrDefault();
        //}

    }
}