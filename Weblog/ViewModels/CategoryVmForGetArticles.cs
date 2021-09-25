namespace Weblog.ViewModels
{
    public class CategoryVmForGetArticles
    {
        public CategoryVmForGetArticles(int articleId, int page, int perPage)
        {
            ArticleId = articleId;
            Page = page;
            PerPage = perPage;
        }
        public int ArticleId { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }

        public CategoryVmForGetArticles()
        {
        } 
    }
}
