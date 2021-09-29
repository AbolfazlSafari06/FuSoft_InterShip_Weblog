using Microsoft.AspNetCore.Http;

namespace Weblog.Requests
{
    public class ArticleRequestEdit
    {
        public ArticleRequestEdit()
        {
            
        }
        public ArticleRequestEdit(int id,string title,string body,string shortDescription, IFormFile image,
            bool status,int categoryId)
        {
            Id = id;
            Title = title;
            Body = body;
            ShortDescription = shortDescription;
            Image = image;
            Status = status;
            CategoryId = categoryId;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Token { get; set; }
        public string Body { get; set; }
        public string ShortDescription { get; set; }
        public IFormFile Image { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
    }
}