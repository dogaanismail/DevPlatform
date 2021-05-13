using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DevPlatform.Domain.Api
{
    public class PostCreateApi
    {
        public string Text { get; set; }
        public IList<IFormFile> Images { get; set; }
        public IFormFile Video { get; set; }
        public bool IsPost { get; set; }
        public bool? IsStory { get; set; }
    }
}
