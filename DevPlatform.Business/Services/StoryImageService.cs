using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Story;
using DevPlatform.Domain.Common;
using DevPlatform.Repository.Generic;
using System;
using System.Collections.Generic;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Store image service implementations
    /// </summary>
    public partial class StoryImageService : IStoryImageService
    {
        #region Fields

        private readonly IRepository<StoryImage> _storyImageRepository;

        #endregion

        #region Ctor
        public StoryImageService(IRepository<StoryImage> storyImageRepository)
        {
            _storyImageRepository = storyImageRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a story image
        /// </summary>
        /// <param name="createImageForStory"></param>
        /// <returns></returns>
        public ResultModel Create(StoryImage createImageForStory)
        {
            if (createImageForStory == null)
                throw new ArgumentNullException(nameof(createImageForStory));

            _storyImageRepository.Insert(createImageForStory);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Create story images by using bulk entities
        /// </summary>
        /// <param name="createImagesForStory"></param>
        /// <returns></returns>
        public ResultModel Create(List<StoryImage> createImagesForStory)
        {
            if (createImagesForStory == null)
                throw new ArgumentNullException(nameof(createImagesForStory));

            _storyImageRepository.Insert(createImagesForStory);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        #endregion
    }
}
