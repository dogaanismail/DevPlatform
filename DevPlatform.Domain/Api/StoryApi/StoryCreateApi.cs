using Microsoft.AspNetCore.Http;

namespace DevPlatform.Domain.Api.StoryApi
{
    public partial class StoryCreateApi
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public IFormFile Video { get; set; }
        public string PhotoUrl {get;set;}
        public string VideoUrl { get; set; }
    }
}
