using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Weblog.Domain.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } 
        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
        public string? Token { get; set; }
        public virtual ICollection<Article> Articles { get; }
        public virtual ICollection<Comment> Comments { get; }
        public User(){}

        public void Update(string name, string email, string newPassword, string currentPassword)
        {
            CheckValidation(name, email, newPassword);

            var _db = new DatabaseContext();
            var duplicate = _db.Users.Count(x => x.Email == email && x.Id != this.Id);
            if (duplicate > 0)
            {
                throw new Exception("آدرس ایمیل وارد شده تکراری می باشد");
            }

            if (newPassword != null)
            {

                if (GetHashString(currentPassword) != this.Password)
                {
                    throw new Exception("رمز عبور فعلی اشتباه می باشد");
                }

                this.Password = GetHashString(newPassword);

            }

            this.Name = name;
            this.Email = email;
        }
        public void Login(string password)
        {
            if (GetHashString(password) != this.Password)
            {
                throw new Exception("رمز عبور اشتباه می باشد");
            }
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            this.Token = token;
        }
        public void UpdateByAdmin(string name, string email, string newPassword, bool isAdmin = false)
        {
            CheckValidation(name, email, newPassword);

            var _db = new DatabaseContext();
            var duplicate = _db.Users.Count(x => x.Email == email && x.Id != this.Id);
            if (duplicate > 0)
            {
                throw new Exception("آدرس ایمیل وارد شده تکراری می باشد");
            }

            if (newPassword != null)
            {
                this.Password = GetHashString(newPassword);
            }

            this.Name = name;
            this.Email = email;
            this.IsAdmin = isAdmin;
        }
        public User(string name, string email, string password, bool isAdmin = false)
        {

            CheckValidation(name, email, password);

            if (password == null) throw new ArgumentNullException("رمز عبور را وارد کنید");


            var _db = new DatabaseContext();
            var duplicate = _db.Users.Any(x => x.Email == email);
            if (duplicate)
            {
                throw new Exception("آدرس ایمیل وارد شده تکراری می باشد");
            }

            this.Name = name;
            this.Email = email;
            this.IsAdmin = isAdmin;
            this.Password = GetHashString(password);
        }
        public static byte[] GetHash(string inputString)
        {
            using HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        public static string GetHashString(string inputString)
        {
            var sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        public static void CheckValidation(string name, string email, string password)
        {
            if (name == null)
            {
                throw new Exception("نام کاربر را وارد کنید");
            }

            if (email == null)
            {
                throw new Exception("ایمیل کاربر را وارد کنید");
            }

            if (name.Length >= 50)
            {
                throw new Exception("نام کاربر نمی تواند بیشتر از 50 کاراکتر باشد");
            }

            if (email.Length >= 200)
            {
                throw new Exception("ایمیل کاربر نمی تواند بیشتر از 200 کاراکتر باشد");
            }

            var validEmail = Regex.Match(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            if (!validEmail.Success)
            {
                throw new Exception("آدرس ایمیل وارد شده معتبر نمی باشد");
            }

            if (string.IsNullOrEmpty(password))
            {

                if (password.Length <= 6)
                {
                    throw new Exception("رمز عبور باید بیشتر از 6 کاراکتر باشد");
                }

                var validPassword = Regex.Match(password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,}$");
                if (!validPassword.Success)
                {
                    throw new Exception("رمز عبور باید شامل حروف، عدد و حداقل یک کاراکتر خاص باشد");
                }
            }
        }
    }
}
