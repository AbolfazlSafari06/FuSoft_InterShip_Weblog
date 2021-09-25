namespace Weblog.Requests
{
    public class CommentCreateRequest
    {
        public CommentCreateRequest(int articleId, int userId, string body, string number,string fullName)
        {
            ArticleId = articleId;
            UserId = userId;
            Body = body;
            Number = number;
            FullName = fullName;
        }

        public CommentCreateRequest()
        {
            
        }

        public string FullName { get; set; }
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
        public string Number { get; set; }
    }
}