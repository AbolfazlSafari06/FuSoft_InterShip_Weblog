namespace Weblog.Requests
{
    public class ArticleListRequest
    {
        public ArticleListRequest(string query, string sort, int perPage, int page,
            int? categoryId, string token)
        {
            Query = query;
            Sort = sort;
            Page = page;
            CategoryId = categoryId;
            Token = token;
            PerPage = perPage;
        }

        public ArticleListRequest()
        {
             
        }

        public string Token { get; set; }
        public int? CategoryId { get; set; }
        public string Query { get; set; }
        public string Sort { get; set; }
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 15;


    }
}
