using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Weblog.Domain.Models;
using Weblog.ViewModels;
using Weblog.Requests;
using Weblog.Services;
using Weblog.Domain;
using System.Linq;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
        public async Task<ActionResult> Index(ArticleListRequest request)
        {

            var users = _db.Users.AsNoTracking();
           
            try
            {

                var user = users.FirstOrDefault(x => x.Token == request.Token);

                if (request is null)
                {
                    throw new Exception("request is null");
                }

                var articleList = _db.Articles.Where(x => x.User.Token == request.Token);
               
                if (request.Query != null)
                {
                    articleList = articleList.Where(x => x.Title.Contains(request.Query.Trim()));
                }

                switch (request.Sort)
                {
                    case "oldest":
                        articleList.OrderBy(x => x.CreatedAt);
                        break;
                    case "latest":
                        articleList.OrderByDescending(x => x.CreatedAt);
                        break;
                    default:
                        break;
                }

                var totalArticleCount = articleList.Count();

                var result = articleList.Skip((request.Page - 1) * request.PerPage)
                    .Take(request.PerPage)
                    .Select(x => new ArticleVm(x.Id, x.Title, x.Body,
                        x.ShortDescription, x.Image, x.CreatedAt,
                        x.UpdatedAt, x.Status, x.CategoryId, x.UserId)).ToList();

                return Ok(new { data = result, lenght = totalArticleCount });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        [Route(Routing.Article.View.Get.List)]
        public ActionResult IndexView(int count)
        {
            try
            {
                var article = _db.Articles.AsNoTracking();

                var totalArticleCount = article.Count();

                var result = article.Take(count)
                    .Select(x => new ArticleVm(x.Id, x.Title, x.Body, x.ShortDescription, x.Image, x.CreatedAt,
                        x.UpdatedAt, x.Status, x.CategoryId, x.UserId)).ToList();

                //foreach (var art in result)
                //{
                //    bool isFileExists = File.Exists($@""); 
                //}


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
        public async Task<ActionResult> Create([FromForm] CreateArticleRequest request)
        {
            try
            {
                if (request is null)
                {
                    throw new Exception("request is null");
                }

                var user = await _db.Users.FirstOrDefaultAsync(u => u.Token == request.Token);
                var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == request.Category);

                if (user == null)
                {
                    return Forbid("شما دسترسی لازم برای اضافه کردن مقاله را ندارید");
                }

                if (request.Category == null)
                {
                    return Forbid("لطفا دسته بندی را انتخاب کنید");

                }
                if (!user.IsAdmin)
                {
                    return Forbid("شما دسترسی لازم برای اضافه کردن مقاله را ندارید");
                }

                if (category == null)
                {
                    return BadRequest("لطفا دسته بندی مقاله را انتخاب کنید");
                }

                var imageName = Path.GetFileName(request.Image.FileName);
                var imagePath = Path.Combine(_env.WebRootPath, "images\\", imageName);
                string imageroute;
                using (var imageSteam = new FileStream(imagePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(imageSteam);
                    imageroute = imageSteam.Name;
                }

                //var image = await _fs.Store(request.Image);
                var article = new Article(request.Title, request.Body, request.ShortDescription,
                    imageroute, request.Status, user, category);

                _db.Articles.Add(article);
                await _db.SaveChangesAsync();


                var result = new ArticleVm(article.Id, article.Title, article.Body,
                    article.ShortDescription, article.Image, article.CreatedAt,
                    article.UpdatedAt, article.Status, article.CategoryId, article.UserId);

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //[HttpPost]
        //[Route(Routing.Article.Post.UploadImage)]
        //public async Task<IActionResult> UploadImage(string name)
        //{
        //    try
        //    {
        //        var result = await _fs.Store(name);
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

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
                    article.CreatedAt, article.UpdatedAt, article.Status, article.CategoryId, article.UserId);
                //articleVm.User.Articles.Clear();
                //articleVm.Category.Articles.Clear();

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
        public async Task<ActionResult> Edit([FromForm] ArticleRequestEdit request)
        {
            var articles = _db.Articles;

            try
            {
                var article = articles.FirstOrDefault(x => x.Id == request.Id) ?? throw new ArgumentNullException("article not found");

                article.Edit(request.Title, request.Body, request.ShortDescription, request.Image.ToString(), request.Status, request.CategoryId);

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
                article.Comments.Clear();
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
