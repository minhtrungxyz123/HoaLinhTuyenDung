using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuyenDung.Common.Extensions;
using TuyenDung.Data.EF;
using TuyenDung.Data.Entities;
using TuyenDung.Model;

namespace TuyenDung.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly TuyenDungDbContext _context;

        public CategoriesController(TuyenDungDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] CategoryModel request)
        {
            var category = new Category()
            {
                Name = request.Name,
                ParentId = request.ParentId,
                SortOrder = request.SortOrder,
                SeoAlias = request.SeoAlias,
                SeoDescription = request.SeoDescription
            };
            _context.Categories.Add(category);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, request);
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create category failed"));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var categorys = await _context.Categories.ToListAsync();

            var categoryVms = categorys.Select(c => CreateCategoryVm(c)).ToList();

            return Ok(categoryVms);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetCategoriesPaging(string filter, int pageIndex, int pageSize)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter)
                || x.Name.Contains(filter));
            }
            var totalRecords = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var data = items.Select(c => CreateCategoryVm(c)).ToList();

            var pagination = new Pagination<CategoryModel>
            {
                Items = data,
                TotalRecords = totalRecords,
            };
            return Ok(pagination);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new ApiNotFoundResponse($"Category with id: {id} is not found"));

            CategoryModel categoryvm = CreateCategoryVm(category);

            return Ok(categoryvm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryModel request)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new ApiNotFoundResponse($"Category with id: {id} is not found"));

            if (id == request.ParentId)
            {
                return BadRequest(new ApiBadRequestResponse("Category cannot be a child itself."));
            }

            category.Name = request.Name;
            category.ParentId = request.ParentId;
            category.SortOrder = request.SortOrder;
            category.SeoDescription = request.SeoDescription;
            category.SeoAlias = request.SeoAlias;

            _context.Categories.Update(category);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest(new ApiBadRequestResponse("Update category failed"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound(new ApiNotFoundResponse($"Category with id: {id} is not found"));

            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                CategoryModel categoryvm = CreateCategoryVm(category);
                return Ok(categoryvm);
            }
            return BadRequest();
        }

        private static CategoryModel CreateCategoryVm(Category category)
        {
            return new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                SortOrder = category.SortOrder,
                ParentId = category.ParentId,
                NumberOfTickets = category.NumberOfTickets,
                SeoDescription = category.SeoDescription,
                SeoAlias = category.SeoAlias
            };
        }
    }
}