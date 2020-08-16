using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using DevPlatform.Repository.Generic;
using System;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Post service
    /// </summary>
    public partial class PostService : IPostService
    {
        #region Fields
        private readonly IRepository<Post> _postRepository;

        #endregion

        #region Ctor
        public PostService(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="post"></param>
        public virtual void DeletePost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _postRepository.Delete(post);
        }

        /// <summary>
        /// Gets a post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public virtual Post GetById(int postId)
        {
            if (postId == 0)
                return null;

            return _postRepository.GetById(postId);
        }

        /// <summary>
        /// Inserts a post
        /// </summary>
        /// <param name="post"></param>
        public virtual ResultModel InsertPost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _postRepository.Insert(post);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post"></param>
        public virtual void UpdatePost(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _postRepository.Update(post);
        }

        #endregion
    }
}
