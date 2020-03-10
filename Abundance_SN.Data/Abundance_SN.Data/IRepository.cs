using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Data
{
    public interface IRepository
    {
        long GetCount<E>(Func<E, bool> match) where E : class;
        int GetMaxValueBy<E>(Func<E, int> match) where E : class;
        long GetMaxValueBy<E>(Func<E, long> match) where E : class;
        ICollection<E> GetAll<E>() where E : class;
        E GetBy<E>(object id) where E : class;
        E GetSingleBy<E>(Expression<Func<E, bool>> match) where E : class;
        ICollection<E> FindAll<E>(Expression<Func<E, bool>> match) where E : class;
        ICollection<E> GetBy<E>(Expression<Func<E, bool>> filter = null, Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = null, string includeProperties = "") where E : class;
        E Add<E>(E e) where E : class;
        int Add<E>(ICollection<E> es) where E : class;
        void Delete<E>(E e) where E : class;
        void Delete<E>(Expression<Func<E, bool>> predicate) where E : class;
        void Delete<E>(List<E> es) where E : class;
        void Delete<E>(object id) where E : class;
        void Update<E>(E e) where E : class;
        void Update<E>(List<E> es) where E : class;
        int Count<E>() where E : class;
        int Save();


        void Dispose();
    }
}
