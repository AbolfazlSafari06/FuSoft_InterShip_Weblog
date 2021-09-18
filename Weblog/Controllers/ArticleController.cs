using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Domain;
using Weblog.Domain.Models;
using Weblog.Requests;
using Weblog.ViewModels;

namespace Weblog.Controllers
{
    [Route(Routing.Article.Base)]
    public class ArticleController : Controller
    {
        private readonly DatabaseContext _db;
        public ArticleController()
        {
            _db = new DatabaseContext();
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
                var result = article.Skip((request.Page - 1) * request.PerPage).Take(request.PerPage)
                    .Select(x => new ArticleVm(x.Title, x.Body, x.ShortDescription, x.Image, x.CreatedAt,
                        x.UpdatedAt, x.Status, x.CategoryId)).ToList();


                return Ok(result);

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
                    throw new Exception(" title is duplicated");

                var newArticle = new Article(request.Title, request.Body, request.ShortDescription, request.Image, request.CreatedAt, request.UpdatedAt, request.Status, request.UserId, request.CategoryId);
                _db.Articles.Add(newArticle);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        // GET: ArticleController/Details/5
        [HttpGet]
        [Route(Routing.Article.Get.Detail)]
        public ActionResult Details(int id)
        {
            var articles = _db.Articles.AsNoTracking();
            try
            {
                var article = articles.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentNullException("article not found");
                return Ok(article);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [Route(Routing.Article.Post.Edit)]
        public ActionResult Edit([FromBody] ArticleVm request, int id)
        {
            var articles = _db.Articles.AsNoTracking();

            try
            {
                var article = articles.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentNullException("article not found");

                article.Edit(request.Title, request.Body, request.ShortDescription, request.Image, request.CreatedAt, request.UpdatedAt, request.Status, request.CategoryId);

                _db.SaveChanges();

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
