using System;
using System.Collections.Generic;

namespace DevPlatform.Domain.Dto.StoryDto
{
    public partial class StoryListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserPhoto { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? StoryType { get; set; }
        public List<StoryCommentListDto> Comments { get; set; }

        //TODO: Storyikes might be implemented here.

        //storylikes
    }
}
