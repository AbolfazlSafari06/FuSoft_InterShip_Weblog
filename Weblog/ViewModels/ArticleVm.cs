using System;

namespace Weblog.ViewModels
{
    public class ArticleVm
    {
        public ArticleVm(string title, string body, string shortDescription, string image, DateTime createdAt, DateTime updatedAt, string status, int categoryId)
        {
            Title = title;
            Body = body;
            ShortDescription = shortDescription;
            Image = image;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Status = status;
            CategoryId = categoryId;
        }

        public string Title { get; set; }
        public int CategoryId { get; set; } 
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; }
    }
}
