namespace Weblog.Requests
{
    public class CreateCategoryRequest
    {
        public int? ParentId { get; set; }
        public int order { get; set; }
        public string Title { get; set; }

        public CreateCategoryRequest(int? parentId, string title, int order)
        {
            ParentId = parentId;
            Title = title;
            this.order = order;
        }

    }
}
