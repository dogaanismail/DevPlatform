using System;

namespace DevPlatform.Domain.Dto
{
    public class AppUserDetailDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
