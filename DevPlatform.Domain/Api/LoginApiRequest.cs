namespace DevPlatform.Domain.Api
{
    public class LoginApiRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
