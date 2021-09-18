namespace Weblog.Requests
{
    public class UpdateCategoryRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int  Order { get; set; }
        public int? ParentId { get; set; }

        public UpdateCategoryRequest(int id ,string title, int order, int? parentId)
        {
            Id = id;
            Title = title;
            Order = order;
            ParentId = parentId;
        }
        public UpdateCategoryRequest()
        {

        }
    }
}
