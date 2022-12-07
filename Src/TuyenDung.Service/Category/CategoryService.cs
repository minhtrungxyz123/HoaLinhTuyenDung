using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuyenDung.Common.XBaseModel;
using TuyenDung.Data.EF;
using TuyenDung.Data.Entities;
using TuyenDung.Model;

namespace TuyenDung.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly TuyenDungDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(TuyenDungDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> DeletesAsync(IEnumerable<int> ids)
        {
            foreach (var entity in ids)
            {
                var item = await _context.Categories.FindAsync(entity);

                _context.Categories.Remove(item);
            }
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<IList<CategoryModel>> GetAll()
        {
            var query = from c in _context.Categories
                        select new { c };
            var entity = await query.Select(x => new CategoryModel()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                SeoAlias = x.c.SeoAlias,
                SeoDescription = x.c.SeoDescription,
                SortOrder = x.c.SortOrder,
                ParentId = x.c.ParentId,
                NumberOfTickets = x.c.NumberOfTickets
            }).ToListAsync();

            return entity;
        }

        public async Task<CategoryModel> GetById(int id)
        {
            var entity = await _context.Categories.FindAsync(id);

            var entityModel = new CategoryModel()
            {
                Id = entity.Id,
                SeoAlias = entity.SeoAlias,
                Name = entity.Name,
                NumberOfTickets = entity.NumberOfTickets,
                ParentId = entity.ParentId,
                SeoDescription = entity.SeoDescription,
                SortOrder = entity.SortOrder
            };
            return entityModel;
        }

        public IPagedList<CategoryModel> GetPaging(CategorySearchContext ctx)
        {
            ctx.Keywords = ctx.Keywords?.Trim();

            var query = from u in _context.Categories
                        join c in _context.Categories on u.Id equals c.ParentId into pt
                        from tp in pt.DefaultIfEmpty()
                        select new CategoryModel
                        {
                            Id = u.Id,
                            SeoDescription = u.SeoDescription,
                            SeoAlias = u.SeoAlias,
                            Name = u.Name,
                            NumberOfTickets = u.NumberOfTickets,
                            ParentId = u.ParentId,
                            SortOrder = u.SortOrder,
                            ParentName = tp.Name
                        };

            if (!string.IsNullOrEmpty(ctx.Keywords))
            {
                query = query.Where(x => x.Name.Contains(ctx.Keywords)
                || x.SeoDescription.Contains(ctx.Keywords));
            }

            query =
                from p in query
                orderby p.Name
                select p;

            return new PagedList<CategoryModel>(query, ctx.PageIndex, ctx.PageSize);
        }

        public async Task<int> InsertAsync(CategoryModel entity)
        {
            var result = _mapper.Map<Category>(entity);
            await _context.Categories.AddAsync(result);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(CategoryModel entity)
        {
            var item = await _context.Categories.FindAsync(entity.Id);
            item.SeoAlias = entity.SeoAlias;
            item.Name = entity.Name;
            item.NumberOfTickets = entity.NumberOfTickets;
            item.ParentId = entity.ParentId;
            item.SeoDescription = entity.SeoDescription;
            item.SortOrder = entity.SortOrder;

            _context.Categories.Update(item);
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}