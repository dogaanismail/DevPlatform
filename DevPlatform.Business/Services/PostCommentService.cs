using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using DevPlatform.Repository.Extensions;
using DevPlatform.Repository.Generic;
using System;
using System.Collections.Generic;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Post comment service
    /// </summary>
    public class PostCommentService : IPostCommentService
    {
        #region Fields
        private readonly IRepository<PostComment> _postCommentRepository;
        #endregion

        #region Ctor
        public PostCommentService(IRepository<PostComment> postCommentRepository)
        {
            _postCommentRepository = postCommentRepository;
        }
        #endregion

        /// <summary>
        /// Creates post comment
        /// </summary>
        /// <param name="createComment"></param>
        /// <returns></returns>
        public ResultModel Create(PostComment createComment)
        {
            if (createComment == null)
                throw new ArgumentNullException(nameof(createComment));

            _postCommentRepository.Insert(createComment);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Deletes a post comment
        /// </summary>
        /// <param name="deleteComment"></param>
        /// <returns></returns>
        public ResultModel Delete(PostComment deleteComment)
        {
            if (deleteComment == null)
                throw new ArgumentNullException(nameof(deleteComment));

            _postCommentRepository.Delete(deleteComment);
            return new ResultModel { Status = true, Message = "Delete Process Success ! " };
        }

        /// <summary>
        /// Updates a post comment
        /// </summary>
        /// <param name="updateComment"></param>
        /// <returns></returns>
        public ResultModel Update(PostComment updateComment)
        {
            if (updateComment == null)
                throw new ArgumentNullException(nameof(updateComment));

            _postCommentRepository.Update(updateComment);
            return new ResultModel { Status = true, Message = "Update Process Success ! " };
        }

        /// <summary>
        /// Returnsa post comment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PostComment GetById(int id)
        {
            return _postCommentRepository.GetById(id);
        }

        /// <summary>
        /// Returns a list of post comment by post id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public List<PostComment> GetPostCommentsByPostId(int postId)
        {
            return _postCommentRepository.GetList(x => x.PostId == postId, x => x.Include(t => t.CreatedUser).ThenInclude(e => e.UserDetail));
        }

    }
}
