using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Weblog.Domain.Models;
using Weblog.ViewModels;
using Weblog.Requests;
using Weblog.Services;
using Weblog.Domain;
using System.Linq;
using System;

namespace Weblog.Controllers
{
    [Route(Routing.Article.Base)]
    public class ArticleController : Controller
    {
        private readonly DatabaseContext _db;
        private readonly FileHandlerService _fs;
        private readonly IHostingEnvironment _env;
        public ArticleController(IHostingEnvironment env)
        {
            _db = new DatabaseContext();
            this._fs = new FileHandlerService(env);
            _env = env;
        }

        // GET: ArticleController
        [HttpGet]
        public ActionResult Index(ArticleListRequest request)
        {
            try
            {
                var article = _db.Articles.AsNoTracking();
                if (request is null)
                {
                    throw new Exception("request is null");
                }

                if (request.Query != null)
                {
                    article = article.Where(x => x.Title.Contains(request.Query.Trim()));
                }

                switch (request.Sort)
                {
                    case "oldest":
                        article.OrderBy(x => x.CreatedAt);
                        break;
                    case "latest":
                        article.OrderByDescending(x => x.CreatedAt);
                        break;
                    default:
                        break;
                }

                var totalArticleCount = article.Count(x=>x.UserId == request.UserId);

                var result = article.Where(x=>x.UserId == request.UserId)
                    .Skip((request.Page - 1) * request.PerPage).Take(request.PerPage)
                    .Select(x => new ArticleVm(x.Id, x.Title, x.Body, x.ShortDescription, x.Image, x.CreatedAt,
                        x.UpdatedAt, x.Status, x.CategoryId)).ToList();

                return Ok(new { data = result, lenght = totalArticleCount });

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        [Route(Routing.Category.View.Get.List)]
        public ActionResult IndexView(int count)
        {
            try
            {
                var article = _db.Articles.AsNoTracking();
                
                var totalArticleCount = article.Count();

                var result = article.Take(count)
                    .Select(x => new ArticleVm(x.Id, x.Title, x.Body, x.ShortDescription, x.Image, x.CreatedAt,
                        x.UpdatedAt, x.Status, x.CategoryId)).ToList();

                return Ok(new { data = result, lenght = totalArticleCount });

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: ArticleController/Create

        [HttpPost]
        [Route(Routing.Article.Post.Create)]
        public async Task<ActionResult> Create([FromBody] CreateArticleRequest request)
        {
            var articles = _db.Articles;
            try
            {
                if (request is null)
                    throw new Exception("request is null");
                if (articles.Any(x => x.Title == request.Title))
                    throw new Exception(" مقاله ای با این عنوان وجود دارد");
                var newArticle = new Article(request.Title, request.Body, request.ShortDescription, request.Image, request.CreatedAt, request.UpdatedAt, request.Status, request.UserId, request.CategoryId);

                _db.Articles.Add(newArticle);
                await _db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route(Routing.Article.Post.UploadImage)]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            try
            {
                var result = await _fs.Store(request.Image);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: ArticleController/Details/5
        [HttpGet]
        [Route(Routing.Article.Get.Detail)]
        public ActionResult Details(string id)
        {
            var articles = _db.Articles;
            try
            {
                var article = articles.Find(Int32.Parse(id));
                if (article == null)
                {
                    throw new Exception("کاربر یافت نشد");
                }

                var articleVm = new ArticleVm(Int32.Parse(id), article.Title, article.Body, article.ShortDescription, article.Image,
                    article.CreatedAt, article.UpdatedAt, article.Status, article.CategoryId);
                return Ok(articleVm);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [Route(Routing.Article.Post.Edit)]
        public async Task<ActionResult> Edit([FromBody] ArticleVm request )
        {
            var articles = _db.Articles;

            try
            {
                var article = articles.FirstOrDefault(x => x.Id == request.Id) ?? throw new ArgumentNullException("article not found");

                article.Edit(request.Title, request.Body, request.ShortDescription, request.Image, request.CreatedAt,
                    DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), request.Status, request.CategoryId);

                await _db.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: ArticleController/Delete/5
        [Route(Routing.Article.Post.Delete)]
        public ActionResult Delete(int id)
        {
            var articles = _db.Articles.AsNoTracking();

            try
            {
                var article = articles.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentNullException("article not found");

                _db.Articles.Remove(article);

                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
