using System;

namespace DevPlatform.Domain.ServiceResponseModels.PostCommentService
{
    /// <summary>
    /// Postcomment service response model
    /// </summary>
    public class CreateResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserPhoto { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PostId { get; set; }
    }
}
