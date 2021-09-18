namespace Weblog.Requests.Auth
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public RegisterRequest(string name, string email, string password, string passwordConfirm)
        {
            Name = name; 
            Email = email;
            Password = password;
            PasswordConfirm = passwordConfirm;
        }

        public RegisterRequest()
        { 
        }
    }
}
