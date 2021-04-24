using System.ComponentModel.DataAnnotations;

namespace DevPlatform.Domain.Enumerations
{
    public enum StoryTypeEnum
    {
        [Display(Name = "STORYIMAGE")]
        StoryImage = 1,

        [Display(Name = "STORYVIDEO")]
        StoryVideo = 2,

        [Display(Name = "STORYTEXT")]
        StoryText = 3,

        [Display(Name = "STORYGIF")]
        StoryGif = 4,
    }
}
