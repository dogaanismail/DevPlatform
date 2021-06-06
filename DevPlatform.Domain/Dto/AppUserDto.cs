using System;

namespace DevPlatform.Domain.Dto
{
    public class AppUserDto
    {
        public int AppUserId { get; set; }
        public string UserName { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string UserToken { get; set; }
    }
}
