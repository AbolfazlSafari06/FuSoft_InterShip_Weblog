using System;
using Microsoft.AspNetCore.Http;
using Weblog.Domain.Models;
using Weblog.ViewModels;

namespace Weblog.Requests
{
    public class CreateArticleRequest
    {
        public CreateArticleRequest(string title,string body, string shortDescription, 
            IFormFile image, bool status, string token, int category)
        {
            Title = title;
            Body = body;
            ShortDescription = shortDescription;
            Image = image;
            Status = status;
            Token = token;
            Category = category ;
        }

        public CreateArticleRequest()
        {
            
        }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Category  { get; set; }
        public string Token { get; set; }
        public string ShortDescription { get; set; }
        public IFormFile? Image { get; set; }
        public bool Status { get; set; }



    }
}
