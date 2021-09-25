namespace Weblog.Response
{
    public class CommentListRequest
    {
        public CommentListRequest(int UserId,string sort, int page, int perPage)
        {
            this.UserId = UserId;
            Sort = sort;
            Page = page;
            PerPage = perPage;
        }

        public CommentListRequest()
        {
            
        }

        public int UserId { get; set; }
        public string Sort { get; set; }
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 30;
    }
}