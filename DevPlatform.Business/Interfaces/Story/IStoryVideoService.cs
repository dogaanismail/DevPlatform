using DevPlatform.Core.Domain.Story;
using DevPlatform.Domain.Common;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces.Story
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
        Task<ResultModel> CreateAsync(StoryVideo createVideoForStory);
    }
}
