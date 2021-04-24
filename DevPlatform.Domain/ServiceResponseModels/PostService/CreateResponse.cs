using DevPlatform.Domain.Dto;
using System;
using System.Collections.Generic;
using StoryCreateResponse = DevPlatform.Domain.ServiceResponseModels.StoryService.CreateResponse;

namespace DevPlatform.Domain.ServiceResponseModels.PostService
{
    /// <summary>
    /// Postservice response model
    /// </summary>
    public class CreateResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserPhoto { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? PostType { get; set; }
        public List<PostCommentListDto> Comments { get; set; }
        public StoryCreateResponse StoryCreateResponse { get; set; }

        //TODO: PostLikes might be implemented here.

        //postLikes
    }
}
