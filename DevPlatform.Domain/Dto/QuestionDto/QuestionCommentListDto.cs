using System;

namespace DevPlatform.Domain.Dto.QuestionDto
{
    public partial class QuestionCommentListDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserPhoto { get; set; }
        public DateTime CreatedDate { get; set; }
        public int QuestionId { get; set; }
    }
}
