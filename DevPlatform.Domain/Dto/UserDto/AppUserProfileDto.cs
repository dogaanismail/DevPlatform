using System.Collections.Generic;

namespace DevPlatform.Domain.Dto.UserDto
{
    public class AppUserProfileDto
    {
        public AppUserDetailDto AppUserDetail { get; set; }
        public IEnumerable<PostListDto> UserPosts { get; set; }
    }
}
