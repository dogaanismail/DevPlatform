using DevPlatform.Business.Interfaces.Portal;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using DevPlatform.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services.Portal
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
        public virtual async Task<ResultModel> CreateAsync(PostImage createImageForPost)
        {
            if (createImageForPost == null)
                throw new ArgumentNullException(nameof(createImageForPost));

            await _postImageRepository.InsertAsync(createImageForPost);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Creates post images by using bulk entities
        /// </summary>
        /// <param name="createImageForPost"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateAsync(List<PostImage> createImageForPost)
        {
            if (createImageForPost == null)
                throw new ArgumentNullException(nameof(createImageForPost));

            await _postImageRepository.InsertAsync(createImageForPost);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }
    }
}
