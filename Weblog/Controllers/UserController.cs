using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weblog.Domain;
using Weblog.Domain.Models;
using Weblog.Requests;
using Weblog.Response.User;
using Weblog.ViewModels;

namespace Weblog.Controllers
{
    [Route(Routing.Users.BaseUser)]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _db;
        public UserController()
        {
            this._db = new DatabaseContext();
        }
        [HttpGet]
        [Route(Routing.Users.Get.Users)]
        public IActionResult Index(UserListRequest request)
        {
            try
            {
                var users = _db.Users.AsNoTracking();

                if (request.Query != null)
                {
                    users = users.Where(x =>
                        x.Name.Contains(request.Query) || x.Email.Contains(request.Query));
                }

                users = request.Sort switch
                {
                    "oldest" => users.OrderBy(x => x.Id),
                    "latest" => users.OrderByDescending(x => x.Id),
                    _ => users.OrderByDescending(x => x.Id)
                };


                var usersCount = users.Count();

                var result = users.Skip((request.Page - 1) * request.PerPage).Take(request.PerPage)
                    .Select(x => new UserVm(x.Id, x.Name, x.Email, x.IsAdmin))
                    .ToList();

                return Ok(new ListOfUserResponse(usersCount, result));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route(Routing.Users.Get.User)]
        public IActionResult Detail(string id)
        {
            try
            {
                var user = this._db.Users.Find(Int32.Parse(id));
                if (user == null)
                {
                    return BadRequest(new Exception("کاربر یافت نشد"));
                }

                var result = new UserVm(user.Id, user.Name, user.Email, user.IsAdmin);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route(Routing.Users.Post.Create)]
        public IActionResult Create([FromBody] CreateUserRequest request)
        {
            try
            {
                if (_db.Users.Any(x => x.Email == request.Email && x.Name == request.Name))
                {
                    throw new Exception("مشخصات این  کاربر تکراری میباشد.");
                }
                var user = new User(request.Name, request.Email, request.Password);
                this._db.Users.Add(user);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route(Routing.Users.Post.Update)]
        public IActionResult Update([FromBody] UpdateUserRequest request)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == request.Id);
            if (user == null)
            {
                return NotFound("user Not Found");
            }
            user.UpdateByAdmin(request.Name, request.Email, request.Password, request.IsAdmin);
            _db.SaveChanges();
            return Ok(new UserVm(user.Id, user.Name, user.Email, user.IsAdmin));
        }
        [HttpPost]
        public IActionResult Store([FromBody] CreateUserRequest request)
        {
            try
            {
                var user = new User(request.Name, request.Email, request.Password, request.IsAdmin);
                this._db.Users.Add(user);
                _db.SaveChanges();
                return Ok(new UserVm(user.Id, user.Name, user.Email, user.IsAdmin));

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete]
        [Route(Routing.Users.Post.Dalete)]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return NotFound();
                }
                user.Comments.Clear();
                _db.Users.Remove(user);
                _db.SaveChanges();

                return Ok("user is deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e);
            }
        }
    }
}
