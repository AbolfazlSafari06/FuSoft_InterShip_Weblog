using System;

namespace Weblog.Requests
{
    public class CreateArticleRequest
    {
        public CreateArticleRequest(string title,string body, string shortDescription, string? image, string? status, int userId,int categoryId)
        {
            Title = title;
            Body = body;
            ShortDescription = shortDescription;
            Image = image;
            CreatedAt = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            UpdatedAt = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            Status = status;
            UserId = userId;
            CategoryId = categoryId;
        }

        public string Title { get; set; }
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string ShortDescription { get; set; }
        public string? Image { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string? Status { get; set; }



    }
}
