using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using DevPlatform.Repository.Generic;
using System;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Post video service
    /// </summary>
    public partial class PostVideoService : IPostVideoService
    {
        #region Fields
        private readonly IRepository<PostVideo> _postVideoRepository;
        #endregion

        #region Ctor
        public PostVideoService(IRepository<PostVideo> postVideoRepository)
        {
            _postVideoRepository = postVideoRepository;
        }

        #endregion

        /// <summary>
        /// Creates a post video
        /// </summary>
        /// <param name="createVideoForPost"></param>
        /// <returns></returns>
        public ResultModel Create(PostVideo createVideoForPost)
        {
            if (createVideoForPost == null)
                throw new ArgumentNullException(nameof(createVideoForPost));

            _postVideoRepository.Insert(createVideoForPost);

            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }
    }
}
