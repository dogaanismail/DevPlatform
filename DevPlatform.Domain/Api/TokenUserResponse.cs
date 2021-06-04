using System;

namespace DevPlatform.Domain.Api
{
    public class TokenUserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Expires { get; set; }
    }
}
