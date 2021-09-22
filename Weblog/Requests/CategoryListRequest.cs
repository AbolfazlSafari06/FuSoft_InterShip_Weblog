namespace Weblog.Requests
{
    public class CategoryListRequest
    {
        public CategoryListRequest(int page, int perPage)
        { 
            Page = page;
            PerPage = perPage;
        }

        public CategoryListRequest()
        {
            
        }
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 15;
    }
}