using System;
using System.Collections.Generic;
using System.Linq;
using Weblog.Domain.Models;

namespace Weblog.ViewModels
{
    public class CategoryWithChildrenVm
    {
        public CategoryWithChildrenVm(int id, string title, int order, ICollection<Category> children, int? parentId)
        {
            Id = id;
            Title = title;
            Order = order;
            ParentId = parentId;
        } 
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }

    }
}
