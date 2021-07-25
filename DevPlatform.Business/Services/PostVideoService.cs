using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using DevPlatform.Repository.Generic;
using System;
using System.Threading.Tasks;

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

        #region Methods

        /// <summary>
        /// Creates a post video
        /// </summary>
        /// <param name="createVideoForPost"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateAsync(PostVideo createVideoForPost)
        {
            if (createVideoForPost == null)
                throw new ArgumentNullException(nameof(createVideoForPost));

            await _postVideoRepository.InsertAsync(createVideoForPost);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        #endregion
    }
}
