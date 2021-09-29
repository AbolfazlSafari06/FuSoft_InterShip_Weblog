using System;
using Weblog.Domain.Models;

namespace Weblog.ViewModels
{
    public class ArticleVm
    {
        public ArticleVm(int id, string title, string body, string shortDescription,
            string image, DateTime createdAt, DateTime? updatedAt, bool status,
            int? categoryId, int userId)
        {
            //TODO :FIX IMAGE URL
            //string imageUri = null;
            //if (image != null)
            //{
            //    var uri = new System.Uri(image);
            //    imageUri = uri.AbsoluteUri;
            //}
            Id = id;
            Title = title;
            Body = body;
            ShortDescription = shortDescription;
            Image = image;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Status = status;
            CategoryId = categoryId;
            UserId = userId;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public int? CategoryId { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Status { get; set; }
    }
}
