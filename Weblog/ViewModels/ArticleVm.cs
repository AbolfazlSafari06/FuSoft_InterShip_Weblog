using System;

namespace Weblog.ViewModels
{
    public class ArticleVm
    {
        public ArticleVm(int id, string title, string body, string shortDescription, string image, string createdAt, string updatedAt, string status, int? categoryId)
        {
            Id = id;
            Title = title;
            Body = body;
            ShortDescription = shortDescription;
            Image = image;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Status = status;
            CategoryId = categoryId; 
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? CategoryId { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Status { get; set; } 
    }
}
