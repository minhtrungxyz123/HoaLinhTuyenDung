using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuyenDung.Data.EF;

namespace TuyenDung.Api.Controllers
{
    public class StatisticsController : BaseController
    {
        private readonly TuyenDungDbContext _context;

        public StatisticsController(TuyenDungDbContext context)
        {
            _context = context;
        }

        [HttpGet("monthly-comments")]
        public async Task<IActionResult> GetMonthlyNewComments(int year)
        {
            var data = await _context.Comments.Where(x => x.CreateDate.Date.Year == year)
                .GroupBy(x => x.CreateDate.Date.Month)
                .OrderBy(x => x.Key)
                .Select(g => new MonthlyCommentsVm()
                {
                    Month = g.Key,
                    NumberOfComments = g.Count()
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("monthly-newkbs")]
        public async Task<IActionResult> GetMonthlyNewKbs(int year)
        {
            var data = await _context.KnowledgeBases.Where(x => x.CreateDate.Date.Year == year)
                .GroupBy(x => x.CreateDate.Date.Month)
                .Select(g => new MonthlyNewKbsVm()
                {
                    Month = g.Key,
                    NumberOfNewKbs = g.Count()
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("monthly-registers")]
        public async Task<IActionResult> GetMonthlyNewRegisters(int year)
        {
            var data = await _context.Users.Where(x => x.CreateDate.Date.Year == year)
               .GroupBy(x => x.CreateDate.Date.Month)
               .Select(g => new MonthlyNewKbsVm()
               {
                   Month = g.Key,
                   NumberOfNewKbs = g.Count()
               })
               .ToListAsync();

            return Ok(data);
        }
    }

    public class MonthlyCommentsVm
    {
        public int Month { get; set; }

        public int NumberOfComments { get; set; }
    }

    public class MonthlyNewKbsVm
    {
        public int Month { get; set; }

        public int NumberOfNewKbs { get; set; }
    }
}