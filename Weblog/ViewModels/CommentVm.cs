namespace Weblog.ViewModels
{
    public class CommentVm
    {
        public CommentVm(int id,string body, byte isPublished, int? articleId, int? userId,string fullName)
        {
            Id = id;
            Body = body;
            IsPublished = isPublished;
            ArticleId = articleId;
            UserId = userId;
            FullName = fullName;
        }

        public int Id { get; set; }
        public string Body { get; set; }
        public byte IsPublished { get; set; }
        public int? ArticleId { get; set; }
        public int? UserId { get; set; }
        public string FullName { get; set; }
    }
}