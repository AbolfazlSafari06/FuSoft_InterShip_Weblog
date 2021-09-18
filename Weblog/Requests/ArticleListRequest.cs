namespace Weblog.Requests
{
    public class ArticleListRequest
    {
        public ArticleListRequest(string query, string sort, int perPage, int page, int? categoryId)
        {
            Query = query;
            Sort = sort;
            Page = page;
            CategoryId = categoryId;
            PerPage = perPage;
        }

        public ArticleListRequest( )
        { 
        }

        public int? CategoryId { get; set; }
        public string Query { get; set; }
        public string Sort { get; set; }
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 15;


    }
}
