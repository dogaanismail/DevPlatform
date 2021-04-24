using DevPlatform.Core.Domain.Story;
using DevPlatform.Domain.Common;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Story video service interface
    /// </summary>
    public partial interface IStoryVideoService
    {
        /// <summary>
        /// Creates a story videos for a story
        /// </summary>
        /// <param name="createVideoForStory"></param>
        /// <returns></returns>
        ResultModel Create(StoryVideo createVideoForStory);
    }
}
