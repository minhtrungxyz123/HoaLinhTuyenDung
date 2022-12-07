using System.Linq.Expressions;
using TuyenDung.Data.Entities;
using TuyenDung.Data.UnitOfWork;

namespace TuyenDung.Data.RepositoryEF
{
    public partial interface IRepositoryEF<T> where T : BaseEntity
    {
        public IUnitOfWork UnitOfWork { get; }

        Task<T> GetFirstAsync(int id);

        Task<T> GetFirstAsyncAsNoTracking(int id);

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> ListAllAsync();

        void Update(T entity);

        void Delete(T entity);

        void BulkInsert(IList<T> listEntity);

        Task BulkInsertAsync(IList<T> listEntity);

        void BulkUpdate(IList<T> listEntity);

        Task BulkUpdateAsync(IList<T> listEntity);

        void BulkDelete(IList<T> listEntity);

        Task BulkDeleteAsync(IList<T> listEntity);

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int records = 0,
               string includeProperties = "");

        public Task<IEnumerable<T>> GetAync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int records = 0,
        string includeProperties = "");
    }
}