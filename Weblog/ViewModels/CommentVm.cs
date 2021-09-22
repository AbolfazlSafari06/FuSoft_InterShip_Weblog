namespace Weblog.ViewModels
{
    public class CommentVm
    {
        public CommentVm(int id,string body, byte isPublished, int? articleId, int? userId)
        {
            Id = id;
            Body = body;
            IsPublished = isPublished;
            ArticleId = articleId;
            UserId = userId;
        }

        public int Id { get; set; }
        public string Body { get; set; }
        public byte IsPublished { get; set; }
        public int? ArticleId { get; set; }
        public int? UserId { get; set; }
    }
}