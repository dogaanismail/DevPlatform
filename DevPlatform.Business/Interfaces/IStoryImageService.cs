using DevPlatform.Core.Domain.Story;
using DevPlatform.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Story image service interface
    /// </summary>
    public partial interface IStoryImageService
    {
        /// <summary>
        /// Creates a story image for a story
        /// </summary>
        /// <param name="createImageForStory"></param>
        /// <returns></returns>
        Task<ResultModel> Create(StoryImage createImageForStory);

        /// <summary>
        /// Creates story images by using bulk entities
        /// </summary>
        /// <param name="createImagesForStory"></param>
        /// <returns></returns>
        Task<ResultModel> Create(List<StoryImage> createImagesForStory);
    }
}
