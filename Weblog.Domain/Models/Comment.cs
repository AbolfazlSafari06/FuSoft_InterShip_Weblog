using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weblog.Domain.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(1000,ErrorMessage = "نظر بیش از 500 حرف است.")]
        public string Body { get; set; }
        public string FullName { get; set; }
        public byte IsPublished { get; set; }
        public int? ArticleId { get; set; } 
        public int? UserId { get; set; } 
        public virtual User User { get; set; } 
        public virtual Article Article { get; set; }
        public Comment(string body, byte isPublished, int articleId,int? userId, string fullName)
        {
            Body = body;
            IsPublished = isPublished;
            ArticleId = articleId;
            UserId = userId;
            FullName = fullName;
        }

        public Comment(string fullName)
        {
            FullName = fullName;
        }

        public void Confirm()
        {
            this.IsPublished = 1;
        }
    }
}
