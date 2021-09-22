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
        [MaxLength(500 ,ErrorMessage = "نظر بیش از 500 حرف است.")]
        public string Body { get; set; }
        public byte IsPublished { get; set; }
        public int? ArticleId { get; set; } 
        public int? UserId { get; set; } 
        public virtual User User { get; set; } 
        public virtual Article Article { get; set; }
        public Comment(string body, byte isPublished, int articleId,int? userId)
        {
            Body = body;
            IsPublished = isPublished;
            ArticleId = articleId;
            UserId = userId; 
        }

        public Comment()
        {
            
        }

        public void Confirm()
        {
            this.IsPublished = 1;
        }
    }
}
