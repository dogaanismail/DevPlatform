using System.ComponentModel.DataAnnotations;

namespace DevPlatform.Domain.Enumerations
{
    public enum PostTypeEnum
    {
        [Display(Name = "POSTIMAGE")]
        PostImage = 1,

        [Display(Name = "POSTVIDEO")]
        PostVideo = 2,

        [Display(Name = "POSTTEXT")]
        PostText = 3,

        [Display(Name = "POSTGIF")]
        PostGif = 3,
    }
}
