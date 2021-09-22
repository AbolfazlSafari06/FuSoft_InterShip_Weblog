namespace Weblog.Controllers
{
    public class CommentCreateRequest
    {
        public CommentCreateRequest(int articleId, int userId, string body, string number)
        {
            ArticleId = articleId;
            UserId = userId;
            Body = body;
            Number = number;
        }

        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
        public string Number { get; set; }
    }
}