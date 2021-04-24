﻿using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Story;
using DevPlatform.Domain.Common;
using DevPlatform.Repository.Generic;
using System;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Story video service implementations
    /// </summary>
    public partial class StoryVideoService : IStoryVideoService
    {
        #region Fields
        private readonly IRepository<StoryVideo> _storyVideoRepository;
        #endregion

        #region Ctor

        public StoryVideoService(IRepository<StoryVideo> storyVideoRepository)
        {
            _storyVideoRepository = storyVideoRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a story video 
        /// </summary>
        /// <param name="createVideoForStory"></param>
        /// <returns></returns>
        public ResultModel Create(StoryVideo createVideoForStory)
        {
            if (createVideoForStory == null)
                throw new ArgumentNullException(nameof(createVideoForStory));

            _storyVideoRepository.Insert(createVideoForStory);

            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        #endregion
    }
}
