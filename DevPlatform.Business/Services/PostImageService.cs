using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using DevPlatform.Repository.Generic;
using System;
using System.Collections.Generic;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Post image service
    /// </summary>
    public partial class PostImageService : IPostImageService
    {
        #region Fields
        private readonly IRepository<PostImage> _postImageRepository;
        #endregion

        #region Ctor
        public PostImageService(IRepository<PostImage> postImageRepository)
        {
            _postImageRepository = postImageRepository;
        }

        #endregion

        /// <summary>
        /// Creates a post image
        /// </summary>
        /// <param name="createImageForPost"></param>
        /// <returns></returns>
        public ResultModel Create(PostImage createImageForPost)
        {
            if (createImageForPost == null)
                throw new ArgumentNullException(nameof(createImageForPost));

            _postImageRepository.Insert(createImageForPost);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Creates posts by using bulk entities
        /// </summary>
        /// <param name="createImageForPost"></param>
        /// <returns></returns>
        public ResultModel Create(List<PostImage> createImageForPost)
        {
            if (createImageForPost == null)
                throw new ArgumentNullException(nameof(createImageForPost));

            _postImageRepository.Insert(createImageForPost);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }
    }
}
