using DevPlatform.Domain.Dto.StoryDto;
using System;
using System.Collections.Generic;

namespace DevPlatform.Domain.Dto
{
    public class PostListDto
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
        public StoryListDto StoryListDto { get; set; }

        //TODO: PostLikes might be implemented here!
    }
}
