using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TuyenDung.Common.Extensions;
using TuyenDung.Data.EF;
using TuyenDung.Data.Entities;
using TuyenDung.Model;

namespace TuyenDung.Service
{
    public class AuthencateService : IAuthencateService
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        private readonly TuyenDungDbContext _context;
        private readonly IMapper _mapper;

        public AuthencateService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IConfiguration config,
            TuyenDungDbContext context,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _context = context;
            _mapper = mapper;
        }

        #endregion Fields

        #region Authencate

        public async Task<ApiResult<string>> Authencate(LoginModel request)
        {
            var user = await _userManager.FindByNameAsync(request.AccountName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var query = from p in _context.Permissions
                        join c in _context.Commands
                        on p.CommandId equals c.Id
                        join f in _context.Functions
                        on p.FunctionId equals f.Url
                        join r in _roleManager.Roles on p.RoleId equals r.Id
                        where roles.Contains(r.Name)
                        select f.Url + "_" + c.Id;
            var menuItem = await query.Distinct().ToListAsync();

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.AccountName),
                new Claim("Id", user.Id),
                new Claim(SystemConstants.Claims.MenuItem, JsonConvert.SerializeObject(menuItem)),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        #endregion Authencate
    }
}