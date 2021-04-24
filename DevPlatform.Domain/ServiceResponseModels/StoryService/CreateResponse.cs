using System;

namespace DevPlatform.Domain.ServiceResponseModels.StoryService
{
    /// <summary>
    /// Story create response model
    /// </summary>
    public class CreateResponse
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

        //TODO: Storylikes might be implemented here.
    }
}
