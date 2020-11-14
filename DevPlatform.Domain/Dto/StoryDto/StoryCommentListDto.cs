using System;

namespace DevPlatform.Domain.Dto.StoryDto
{
    public partial class StoryCommentListDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserPhoto { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StoryId { get; set; }
    }
}
