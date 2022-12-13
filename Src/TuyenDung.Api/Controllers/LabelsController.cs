using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuyenDung.Common.Extensions;
using TuyenDung.Data.EF;
using TuyenDung.Model;

namespace TuyenDung.Api.Controllers
{
    public class LabelsController : BaseController
    {
        private readonly TuyenDungDbContext _context;

        public LabelsController(TuyenDungDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string id)
        {
            var label = await _context.Labels.FindAsync(id);
            if (label == null)
                return NotFound(new ApiNotFoundResponse($"Label with id: {id} is not found"));

            var labelVm = new LabelModel()
            {
                Id = label.Id,
                Name = label.Name
            };

            return Ok(labelVm);
        }

        [HttpGet("popular/{take:int}")]
        [AllowAnonymous]
        public async Task<List<LabelModel>> GetPopularLabels(int take)
        {

            var query = from l in _context.Labels
                        join lik in _context.LabelInKnowledgeBases on l.Id equals lik.LabelId
                        group new { l.Id, l.Name } by new { l.Id, l.Name } into g
                        select new
                        {
                            g.Key.Id,
                            g.Key.Name,
                            Count = g.Count()
                        };
            var labels = await query.OrderByDescending(x => x.Count).Take(take)
                .Select(l => new LabelModel()
                {
                    Id = l.Id,
                    Name = l.Name
                }).ToListAsync();


            return labels;
        }
    }
}
