using System.Collections.Generic;
using Weblog.Domain.Models;

namespace Weblog.ViewModels
{
    public sealed class CategoryVm
    {
        public CategoryVm(int id, string title, int order, int? parentId, int children)
        {
            Id = id;
            Title = title;
            Order = order;
            ParentId = parentId;
            Children = children;
        }

        public CategoryVm()
        {

        }

        public int Id { get; set; } 
        public string Title { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; } 
        public int Children { get; set; } 

    }
}