using DevPlatform.Domain.Common;
using System.Collections.Generic;

namespace DevPlatform.Domain.Api.QuestionApi
{
    public partial class QuestionCreateApi
    {
        public QuestionCreateApi()
        {
            Tags = new List<TagsModel>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TagsModel> Tags { get; set; }
    }
}
