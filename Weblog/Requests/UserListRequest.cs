namespace Weblog.Requests
{
    public class UserListRequest
    {
        public string Query { get; set; }
        public string Sort { get; set; }
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 15;

        public UserListRequest()
        {

        }

        public UserListRequest(string sort, string query, int page, int perPage)
        {
            Query = query;
            Sort = sort;
            Page = page;
            PerPage = perPage;
        }

    }
}
