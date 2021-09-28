namespace Weblog.Requests
{
    public class CreateCategoryRequest
    {
        public int? ParentId { get; set; }
        public int Order { get; set; } = 0;
        public string Title { get; set; }

        public CreateCategoryRequest(int? parentId, string title, int order)
        {
            ParentId = parentId;
            Title = title;
            this.Order = order;
        }

    }
}
