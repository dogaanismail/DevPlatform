using System;

namespace DevPlatform.Domain.ServiceResponseModels.QuestionCommentService
{
    /// <summary>
    /// Question comment service response model
    /// </summary>
    public partial class CreateResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserPhoto { get; set; }
        public DateTime CreatedDate { get; set; }
        public int QuestionId { get; set; }
    }
}
