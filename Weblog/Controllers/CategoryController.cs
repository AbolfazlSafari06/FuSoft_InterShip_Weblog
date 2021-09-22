using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Weblog.Domain;
using Weblog.Domain.Models;
using Weblog.Requests;
using Weblog.ViewModels;

namespace Weblog.Controllers
{
    [Route(Routing.Category.BaseUser)]
    public class CategoryController : ControllerBase
    {
        private readonly DatabaseContext _db;
        public CategoryController()
        {
            this._db = new DatabaseContext();
        }
        [HttpGet]
        public IActionResult Index(CategoryListRequest request)
        {
            try
            {
                var categories = _db.Categories.AsNoTracking();

                var totalArticleCount = categories.Count();


                if (request is null)
                {
                    throw new Exception("request is null");
                }

                var result = categories
                    .Skip((request.Page - 1) * request.PerPage).Take(request.PerPage)
                    .Select(x => new CategoryVm(x.Id, x.Title, x.Order, x.ParentId, x.Children.Count))
                    .ToList();

                //var result = returnjson(categories.FirstOrDefault(x => x.Id == 1005));

                return Ok(new { data = result, lenght = totalArticleCount });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        [Route(Routing.Category.Get.GetListOfCategories)]
        public IActionResult GetListOfCategory()
        {
            try
            {
                var categories = _db.Categories
                    .Where(c => c.ParentId == null)
                    .OrderBy(c => c.Order)
                    .Select(x => new { x.Id, x.Title })
                    .ToList();

                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet]
        [Route(Routing.Category.Get.Categoriy)]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var category = _db.Categories.FirstOrDefault(x => x.Id == id);
                if (category is null)
                {
                    throw new Exception("category not found");
                }
                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route(Routing.Category.Get.Categoriy)]
        public IActionResult Detail(string id)
        {
            var user = this._db.Users.Find(Int32.Parse(id));
            if (user == null)
            {
                return BadRequest(new Exception("user Not Found"));
            }
            return Ok(user);
        }
        [HttpPost]
        [Route(Routing.Category.Post.Create)]
        public IActionResult Create([FromBody] CreateCategoryRequest request)
        {
            var categories = _db.Categories;
            try
            {
                if (request is null)
                    throw new Exception("request is null");
                if (categories.Any(x => x.Title == request.Title))
                    throw new Exception("this category is exist");

                var category = new Category(request.Title, request.order, request.ParentId);

                categories.Add(category);
                _db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route(Routing.Category.Get.GetaPrents)]
        public async Task<IActionResult> GetParentCategory(string name)
        {
            try
            {
                var categories = _db.Categories.AsNoTracking();
                if (name == null)
                {
                    throw new Exception("name is null");
                }

                var selected = categories.Where(c => c.Title.Contains(name.Trim()));

                if (selected == null)
                    throw new Exception("noResult");

                return Ok(selected.Select(x => new { x.Title, x.Id }));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route(Routing.Category.Post.Update)]
        public IActionResult Update([FromBody] UpdateCategoryRequest request)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Id == request.Id);
            if (category == null)
            {
                return NotFound("user Not Found");
            }
            category.UpdateByAdmin(request.Title, request.Order, request.ParentId);
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route(Routing.Category.Post.Dalete)]
        public IActionResult Delete(int id)
        {
            try
            {
                var category = _db.Categories.FirstOrDefault(x => x.Id == id);
                if (category == null)
                {
                    return NotFound(nameof(category));
                }
                _db.Categories.Remove(category);
                _db.SaveChanges();
                return Ok("دسته بندی با موفقیت پاک شد");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}