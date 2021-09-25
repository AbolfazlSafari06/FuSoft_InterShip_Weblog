using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weblog.Domain.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        public int Order { get; set; } 
        public int? ParentId { get; set; }
        public virtual ICollection<Category>? Children { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public Category(string title,int order,int? parentId)
        {
            Title = title;
            Order = order;
            ParentId = parentId;
        }

        public void UpdateByAdmin(string title , int order ,int? parentId)
        {
            this.Title = title;
            Order = order;
            ParentId = parentId;
        }
    }
}
