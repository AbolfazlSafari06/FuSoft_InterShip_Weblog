using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weblog.Domain;
using Weblog.Domain.Models;
using Weblog.Requests.Auth;
using Weblog.Response.Auth;

namespace Weblog.Controllers
{
    [Route(Routing.Auth.Base)]

    public class AuthController : Controller
    {
        private readonly DatabaseContext _db;
        public AuthController()
        {
            this._db = new DatabaseContext();
        }
        [HttpPost]
        [Route(Routing.Auth.Login)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Email == request.Email);
                if (user is null)
                {
                    throw new Exception("user not found");
                }

                user.Login(request.Password);
                _db.SaveChanges();

                return Ok(new LoginResponse(user.Id, user.Name, user.Email, user.Token, user.IsAdmin));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route(Routing.Auth.Register)]

        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (request.Password != request.PasswordConfirm)
                {
                    return BadRequest("تکرار رمز عبو مطابقت ندارد");
                }

                var user = new User(request.Name, request.Email, request.Password);
                user.Login(request.Password);
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return Ok(new LoginResponse(user.Id, user.Name, user.Email, user.Token, user.IsAdmin));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
