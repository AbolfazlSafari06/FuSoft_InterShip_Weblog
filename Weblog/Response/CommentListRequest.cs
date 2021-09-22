namespace Weblog.Controllers
{
    public class CommentListRequest
    {
        public CommentListRequest(string sort, int page, int perPage)
        { 
            Sort = sort;
            Page = page;
            PerPage = perPage;
        }

        public CommentListRequest()
        {
            
        } 

        public string Sort { get; set; }
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 30;
    }
}