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

                if (request is null)
                {
                    throw new Exception("request is null");
                }
                var categories = _db.Categories.AsNoTracking();

                var totalArticleCount = categories.Count();
                
                var result = categories
                    .Skip((request.Page - 1) * request.PerPage).Take(request.PerPage)
                        .Select(x => new CategoryVm(x.Id, x.Title, x.Order, x.ParentId))
                        .ToList(); 

                return Ok(new { data = result, lenght = totalArticleCount });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
       
        [HttpGet]
        [Route(Routing.Category.View.Get.List)]
        public IActionResult CategoryViewList(int perPage)
        {
            try
            {
                var categories = _db.Categories.AsNoTracking();

                var totalArticleCount = categories.Count();

                var result = categories.Take(perPage)
                    .Select(x => new CategoryVm(x.Id, x.Title, x.Order, x.ParentId))
                    .ToList();

                //var result = returnjson(categories.FirstOrDefault(x => x.Id == 1005));

                return Ok(new { data = result, lenght = totalArticleCount });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        //[HttpGet]
        //[Route(Routing.Category.Get.GetListOfCategories)]
        //public IActionResult GetListOfCategory()
        //{
        //    try
        //    {
        //        var categories = _db.Categories
        //            .Where(c => c.ParentId == null)
        //            .OrderBy(c => c.Order)
        //            .Select(x=>new CategoryVm(x.Id,x.Title,x.Order,x.ParentId))
        //            .ToList();

        //        return Ok(categories);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}
        [HttpGet]
        [Route(Routing.Category.Get.Categoriy)]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var category = _db.Categories.FirstOrDefault(x => x.Id == id);
                if (category is null)
                {
                    throw new Exception("دسته بندی پیدا نشد");
                }

                var result = new CategoryVm(category.Id, category.Title, category.Order, category.ParentId); 

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route(Routing.Category.Get.Categoriy)]
        public IActionResult Detail(int id)
        {
            var category = this._db.Categories.Find(id);
            if (category == null)
            {
                return BadRequest(new Exception("user Not Found"));
            }
            return Ok(category);
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

                return Ok(category);
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

                var selected = categories.Where(c => c.Title.Contains(name.Trim()))
                    .Select(x=>new CategoryVm(x.Id,x.Title,x.Order,x.ParentId));

                if (selected is null)
                    throw new Exception("دسته بندی  ای پیدا نشد");

                return Ok(selected);
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
            try
            {
                var category = _db.Categories.FirstOrDefault(x => x.Id == request.Id);
                if (category == null)
                {
                    return NotFound("user Not Found");
                }
                category.UpdateByAdmin(request.Title, request.Order, request.ParentId);
                _db.SaveChanges();
                return Ok(new CategoryVm(category.Id,category.Title,category.Order,category.ParentId));
            }
            catch (Exception e)
            {
                return  BadRequest(e);
            }
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

                if (category.Children.Count == 0|| category.Articles.Count == 0) throw new Exception("حذف دسته مورد نظر امکان پذیر نمیباشد زیرا دارای زیر دسته و مقاله میباشد");
                _db.Categories.Remove(category);
                _db.SaveChanges();
                return Ok("دسته بندی با موفقیت پاک شد");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}