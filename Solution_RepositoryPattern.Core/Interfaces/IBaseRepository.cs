using Solution_RepositoryPattern.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solution_RepositoryPattern.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        #region final
        Task<IEnumerable<T>> GetAllAsync(string[] includes = null);
        Task<T> AddAsync(T entity);

        Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> match,string[] includes = null);
        #endregion









        T GetById(int id);



        IEnumerable<T> GetAll();

        T Find(Expression<Func<T, bool>> match, string[] includes = null);

        

        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int take, int skip);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, int take, int skip);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take, int? skip, 
                           Expression<Func<T, object>> orderBy = null, string OrderByDirection = OrderBy.Ascending);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, int? take, int? skip,
                         Expression<Func<T, object>> orderBy = null, string OrderByDirection = OrderBy.Ascending);

        T Add(T entity);

       
        IEnumerable<T> AddRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        T Update(T entity);

        Task<T> SingleAsync(Expression<Func<T, bool>> match);

        T Delete(T entity);

    }
}
