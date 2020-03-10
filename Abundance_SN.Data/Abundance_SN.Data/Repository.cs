using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Abundance_SN.Model.Entity;
using System.Data.Entity.Validation;

namespace Abundance_SN.Data
{
    public class Repository : IRepository, IDisposable
    {
        private bool disposed = false;
        protected Abundance_SN_2Entities context;

        public Repository() : this(new Abundance_SN_2Entities()) { }

        public Repository(Abundance_SN_2Entities _context)
        {
            context = _context;
        }

        //new
        public long GetCount<E>(Func<E, bool> match) where E : class
        {
            try
            {
                long count = 0;
                DbSet<E> es = context.Set<E>();
                if (es != null && es.LongCount() > 0)
                {
                    count = context.Set<E>().LongCount(match);
                }

                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public long GetMaxValueBy<E>(Func<E, long> match) where E : class
        {
            try
            {
                long maximum = 0;
                DbSet<E> es = context.Set<E>();
                if (es != null && es.Count() > 0)
                {
                    maximum = context.Set<E>().Max(match);
                }

                return maximum;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetMaxValueBy<E>(Func<E, int> match) where E : class
        {
            try
            {
                int maximum = 0;
                DbSet<E> es = context.Set<E>();
                if (es != null && es.Count() > 0)
                {
                    maximum = context.Set<E>().Max(match);
                }

                return maximum;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<E> GetAll<E>() where E : class
        {
            try
            {
                return context.Set<E>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public E GetBy<E>(object id) where E : class
        {
            try
            {
                return context.Set<E>().Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public E GetSingleBy<E>(Expression<Func<E, bool>> match) where E : class
        {
            try
            {
                return context.Set<E>().SingleOrDefault(match);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<E> FindAll<E>(Expression<Func<E, bool>> match) where E : class
        {
            try
            {
                return context.Set<E>().Where(match).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<E> GetBy<E>(Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class
        {
            try
            {
                IQueryable<E> query = context.Set<E>();
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public E Add<E>(E e) where E : class
        {
            try
            {
                E newE = context.Set<E>().Add(e);
                return newE;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Add<E>(ICollection<E> es) where E : class
        {
            try
            {
                foreach (E e in es)
                {
                    context.Set<E>().Add(e);
                }

                return es.Count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete<E>(E e) where E : class
        {
            try
            {
                DbSet<E> dbSet = context.Set<E>();
                if (context.Entry(e).State == EntityState.Detached)
                {
                    dbSet.Attach(e);
                }

                dbSet.Remove(e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete<E>(Expression<Func<E, bool>> predicate) where E : class
        {
            try
            {
                DbSet<E> dbSet = context.Set<E>();
                IEnumerable<E> records = from x in dbSet.Where<E>(predicate) select x;

                foreach (E e in records)
                {
                    if (context.Entry(e).State == EntityState.Detached)
                    {
                        dbSet.Attach(e);
                    }

                    dbSet.Remove(e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete<E>(List<E> es) where E : class
        {
            try
            {
                DbSet<E> dbSet = context.Set<E>();
                foreach (E e in es)
                {
                    if (context.Entry(e).State == EntityState.Detached)
                    {
                        dbSet.Attach(e);
                    }

                    dbSet.Remove(e);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete<E>(object id) where E : class
        {
            try
            {
                DbSet<E> dbSet = context.Set<E>();
                E e = dbSet.Find(id);

                if (context.Entry(e).State == EntityState.Detached)
                {
                    dbSet.Attach(e);
                }

                dbSet.Remove(e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update<E>(E e) where E : class
        {
            try
            {
                DbSet<E> dbSet = context.Set<E>();
                dbSet.Attach(e);
                context.Entry(e).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update<E>(List<E> es) where E : class
        {
            try
            {
                DbSet<E> dbSet = context.Set<E>();
                foreach (E e in es)
                {
                    dbSet.Attach(e);
                    context.Entry(e).State = EntityState.Modified;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public E Update<E>(E updated, int key) where E : class
        //{
        //    if (updated == null)
        //    {
        //        return null;
        //    }

        //    E existing = context.Set<E>().Find(key);
        //    if (existing != null)
        //    {
        //        context.Entry(existing).CurrentValues.SetValues(updated);
        //        context.SaveChanges();
        //    }

        //    return existing;
        //}

        public int Count<E>() where E : class
        {
            try
            {
                return context.Set<E>().Count();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            catch(DbEntityValidationException ex)
            {
                ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).ToList().ForEach(item => Console.WriteLine("{0}-{1}", item.PropertyName, item.ErrorMessage));
                throw;
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
