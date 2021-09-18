namespace Weblog.ViewModels
{
    public class UserVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } 
        public bool IsAdmin { get; set; }

        public UserVm(int id, string name, string email, bool isAdmin)
        {
            Id = id;
            Name = name;
            Email = email; 
            IsAdmin = isAdmin;
        } 
    }
}
