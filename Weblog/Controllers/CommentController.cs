using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weblog.Domain;
using Weblog.Domain.Models;
using Weblog.Requests;
using Weblog.ViewModels;

namespace Weblog.Controllers
{
    [Route(Routing.Comments.Base)]
    public class CommentController : ControllerBase
    {
        private readonly DatabaseContext _db;
        public CommentController()
        {
            this._db = new DatabaseContext();
        }
        [HttpGet]
        public async Task<IActionResult> Index(CommentListRequest request)
        {
            var comments = _db.Comments.AsNoTracking();

            try
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));

                var totalArticleCount = comments.Count();

                comments = request.Sort switch
                {
                    "0" => comments.Where(x => x.IsPublished == 0),
                    "1" => comments.Where(x => x.IsPublished == 1),
                    //"2" => comments.Where(x => x.IsPublished == 2),
                    _ => comments
                };

                var result = comments
                    .Skip((request.Page - 1) * request.PerPage).Take(request.PerPage)
                    .Select(x => new CommentVm(x.Id,x.Body, x.IsPublished, x.ArticleId, x.UserId));

                return Ok(new { data = result, lenght = totalArticleCount });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route(Routing.Users.Post.Create)]
        public IActionResult Create([FromBody] CommentCreateRequest request)
        {
            try
            {

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var coment = _db.Comments.FirstOrDefault(x => x.Id == id);
                if (coment == null)
                {
                    throw new Exception("نظر یافت نشد");
                }
                _db.Comments.Remove(coment);
                _db.SaveChanges();

                return Ok("نظر با موفقیت پاک شد");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPut]
        public IActionResult Confirm(int id)
        {
            try
            {
                var coment = _db.Comments.FirstOrDefault(x => x.Id == id);
                if (coment == null)
                {
                    throw new Exception("نظر یافت نشد");
                }

                coment.Confirm();
                _db.SaveChanges();

                return Ok("نظر با موفقیت تایید شد");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
