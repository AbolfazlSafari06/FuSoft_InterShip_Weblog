﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weblog.Domain.Models
{
    [Table("Articles")]
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required]

        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Status { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Article(string title, string body, string shortDescription, string image, DateTime createdAt, DateTime updatedAt, string status, int userId, int categoryId)
        {
            Title = title;
            Body = body;
            ShortDescription = shortDescription;
            Image = image;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Status = status;
            UserId = userId;
            CategoryId = categoryId;
        }

        public void Edit(string title, string body, string shortDescription, string image, DateTime createdAt,
            DateTime updatedAt, string status, int categoryId)
        {
            Title = title;
            Body = body;
            ShortDescription = shortDescription;
            Image = image;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Status = status; 
            CategoryId = categoryId;
        }
    }
}