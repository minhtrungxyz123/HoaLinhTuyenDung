using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuyenDung.Data.EF;
using TuyenDung.Model;

namespace TuyenDung.Api.Controllers
{
    public class CommandsController : BaseController
    {
        private readonly TuyenDungDbContext _context;

        public CommandsController(TuyenDungDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommands()
        {
            var user = User.Identity.Name;
            var commands = _context.Commands;

            var commandVms = await commands.Select(u => new CommandModel()
            {
                Id = u.Id,
                Name = u.Name,
            }).ToListAsync();

            return Ok(commandVms);
        }
    }
}