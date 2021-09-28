using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        public Category(string title, int order, int? parentId)
        {
            Validation(title);
            CheckInheritanceinCategory(parentId);
            Title = title;
            Order = order;
            ParentId = parentId;
        }

        public Category()
        {

        }
        public void UpdateByAdmin(string title, int order, int? parentId)
        {
            Validation(title);
            CheckInheritanceinCategory(parentId);
            this.Title = title;
            Order = order;
            ParentId = parentId;
        }

        public void Validation(string title)
        {
            if (title.Length > 200)
            {
                throw new Exception("نام دسته بندی بیشتر از 200 حرف میباشد");
            }
        }

        public void CheckInheritanceinCategory(int? parentId)
        {
            if (this.Children is null) return;
            if (parentId is null) return;
            if (Children.Any(x => x.Id == parentId))
            {
                throw new Exception("خطا در ایجاد دسته بندی");
            }
        }

    }
}
