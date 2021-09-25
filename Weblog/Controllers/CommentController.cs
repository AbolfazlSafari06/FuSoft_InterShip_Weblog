using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weblog.Domain;
using Weblog.Domain.Models;
using Weblog.Requests;
using Weblog.Response;
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
                    .Select(x => new CommentVm(x.Id, x.Body, x.IsPublished, x.ArticleId, x.UserId, x.FullName));

                return Ok(new { data = result, lenght = totalArticleCount });
            }

            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        [Route(Routing.Comments.Get.View)]
        public async Task<IActionResult> Index(CategoryVmForGetArticles request)
        {
            var comments = _db.Comments.AsNoTracking();

            try
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));

                var totalArticleCount = comments.Count();

                var result = comments.Where(x => x.IsPublished == 1 && x.ArticleId == request.ArticleId)
                    .Select(x => new CommentVm(x.Id, x.Body, x.IsPublished, x.ArticleId, x.UserId, x.FullName))
                    .ToList();

                return Ok(new { data = result, lenght = totalArticleCount });

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost]
        [Route(Routing.Comments.Post.Create)]
        public IActionResult Create([FromBody] CommentCreateRequest request)
        {
            try
            {
                if (request is null)
                {
                    throw new Exception();
                }

                if (_db.Comments.Any(x => x.Body == request.Body))
                {
                    throw new Exception("همچین نظری وجود دارد");
                }

                var comment = new Comment(request.Body, 0, request.ArticleId, request.UserId, request.FullName);

                _db.Comments.Add(comment);
                _db.SaveChanges();
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
