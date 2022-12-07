using Microsoft.AspNetCore.Mvc;
using TuyenDung.Model;
using TuyenDung.Service;

namespace TuyenDung.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthencateService _authencateService;

        public LoginController(IAuthencateService authencateService)
        {
            _authencateService = authencateService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authencateService.Authencate(request);

            if (string.IsNullOrEmpty(result.Data))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}