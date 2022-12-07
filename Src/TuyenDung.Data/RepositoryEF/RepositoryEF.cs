using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TuyenDung.Data.EF;
using TuyenDung.Data.Entities;
using TuyenDung.Data.UnitOfWork;

namespace TuyenDung.Data.RepositoryEF
{
    public class RepositoryEF<T> : IRepositoryEF<T> where T : BaseEntity
    {
        private readonly TuyenDungDbContext _context;
        private readonly DbSet<T> _dbSet;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public RepositoryEF(TuyenDungDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>() ?? throw new ArgumentNullException(nameof(_context));
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity is null)
                throw new NotImplementedException(nameof(entity));

            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public virtual IEnumerable<T> Get(
                    Expression<Func<T, bool>> filter = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int records = 0,
                    string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (records > 0 && orderBy != null)
            {
                query = orderBy(query).Take(records);
            }
            else if (orderBy != null && records == 0)
            {
                query = orderBy(query);
            }
            else if (orderBy == null && records > 0)
            {
                query = query.Take(records);
            }

            return query.ToList();
        }

        public virtual void Delete(T entity)
        {
            if (entity is null)
                throw new NotImplementedException(nameof(entity));
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual async Task<T> GetFirstAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<T> GetFirstAsyncAsNoTracking(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _dbSet.Where(x => string.IsNullOrEmpty(x.Id.ToString())).ToListAsync();
        }

        public virtual void Update(T entity)
        {
            if (entity is null)
                throw new NotImplementedException(nameof(entity));
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void BulkInsert(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            _context.BulkInsert(listEntity);
        }

        public async Task<IEnumerable<T>> GetAync(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int records = 0,
          string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (records > 0 && orderBy != null)
            {
                query = orderBy(query).Take(records);
            }
            else if (orderBy != null && records == 0)
            {
                query = orderBy(query);
            }
            else if (orderBy == null && records > 0)
            {
                query = query.Take(records);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task BulkInsertAsync(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            await _context.BulkInsertAsync(listEntity);
        }

        public void BulkUpdate(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            _context.BulkUpdate(listEntity);
        }

        public async Task BulkUpdateAsync(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            await _context.BulkUpdateAsync(listEntity);
        }

        public void BulkDelete(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            _context.BulkDelete(listEntity);
        }

        public async Task BulkDeleteAsync(IList<T> listEntity)
        {
            if (listEntity is null)
                throw new NotImplementedException(nameof(listEntity));
            await _context.BulkDeleteAsync(listEntity);
        }
    }
}