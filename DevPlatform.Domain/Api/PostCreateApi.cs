using Microsoft.AspNetCore.Http;

namespace DevPlatform.Domain.Api
{
    public class PostCreateApi
    {
        public string Text { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile Video { get; set; }
    }
}
