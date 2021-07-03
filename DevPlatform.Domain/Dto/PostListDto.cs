using DevPlatform.Domain.Dto.StoryDto;
using System;
using System.Collections.Generic;

namespace DevPlatform.Domain.Dto
{
    public class PostListDto
    {
        public PostListDto()
        {
            ImageUrlList = new List<string>();
            Comments = new List<PostCommentListDto>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserPhoto { get; set; }
        public List<string> ImageUrlList { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? PostType { get; set; }
        public List<PostCommentListDto> Comments { get; set; }
        public StoryListDto StoryListDto { get; set; }
        public string FancyboxData { get; set; }

        //TODO: PostLikes might be implemented here!
    }
}
