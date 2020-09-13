using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Repository.Extensions;
using DevPlatform.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public virtual void Delete(Post post)
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
        public virtual ResultModel Create(Post post)
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
        public virtual void Update(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _postRepository.Update(post);
        }

        /// <summary>
        /// Returns a post by id as dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PostListDto GetByIdAsDto(int id)
        {
            Post getPost = _postRepository.GetById(id, x => x.Include(y => y.PostImages)
            .Include(a => a.PostVideos).Include(comment => comment.PostComments)
            .ThenInclude(tr => tr.CreatedUser).ThenInclude(hg => hg.UserDetail)
            .Include(s => s.CreatedUser).ThenInclude(t => t.UserDetail));

            PostListDto postListDto = new PostListDto
            {
                Id = getPost.Id,
                Text = getPost.Text,
                CreatedDate = getPost.CreatedDate,
                ImageUrl = getPost.PostImages.Count() == 0 ? "" : getPost.PostImages.FirstOrDefault().ImageUrl, //TODO It can be more photos of the post
                VideoUrl = getPost.PostVideos.Count() == 0 ? "" : getPost.PostVideos.FirstOrDefault().VideoUrl, //TODO It can be more videos of the post
                CreatedByUserName = getPost.CreatedUser == null ? "" : getPost.CreatedUser.UserName,
                CreatedByUserPhoto = getPost.CreatedUser.UserDetail.ProfilePhotoPath,
                PostType = getPost.PostType,
                Comments = getPost.PostComments.ToList().Select(y => new PostCommentListDto
                {
                    Text = y.Text,
                    CreatedDate = y.CreatedDate,
                    Id = y.Id,
                    CreatedByUserName = y.CreatedUser.UserName,
                    CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                    PostId = y.PostId
                }).ToList()
            };

            return postListDto;
            //TODO must be reverted to LoadWith LinqToDB extension method
        }

        /// <summary>
        /// Returns a list of posts as dto
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PostListDto> GetPostList()
        {
            IEnumerable<PostListDto> data = _postRepository.GetList(null, x => x.Include(y => y.PostImages)
            .Include(a => a.PostVideos).Include(comment => comment.PostComments)
            .ThenInclude(tr => tr.CreatedUser).ThenInclude(hg => hg.UserDetail)
            .Include(s => s.CreatedUser).ThenInclude(t => t.UserDetail))
                .Select(p => new PostListDto
                {
                    Id = p.Id,
                    Text = p.Text,
                    CreatedDate = p.CreatedDate,
                    ImageUrl = p.PostImages.Count() == 0 ? "" : p.PostImages.FirstOrDefault().ImageUrl, //TODO It can be more photos of the post
                    VideoUrl = p.PostVideos.Count() == 0 ? "" : p.PostVideos.FirstOrDefault().VideoUrl, //TODO It can be more videos of the post
                    CreatedByUserName = p.CreatedUser == null ? "" : p.CreatedUser.UserName,
                    CreatedByUserPhoto = p.CreatedUser == null ? "" : p.CreatedUser.UserDetail.ProfilePhotoPath,
                    PostType = p.PostType,
                    Comments = p.PostComments.ToList().Select(y => new PostCommentListDto
                    {
                        Text = y.Text,
                        CreatedDate = y.CreatedDate,
                        Id = y.Id,
                        CreatedByUserName = y.CreatedUser.UserName,
                        CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                        PostId = y.PostId
                    }).ToList()

                    //TODO must be reverted to LoadWith LinqToDB extension method
                }).OrderByDescending(sa => sa.CreatedDate).AsEnumerable();

            return data;
        }

        /// <summary>
        /// Returns a list of post by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetUserPostsByUserId(int userId)
        {
            return _postRepository.Find(x => x.CreatedBy == userId, x => x.Include(y => y.PostImages)
            .Include(a => a.PostVideos).Include(b => b.PostComments)
            .ThenInclude(tr => tr.CreatedUser).ThenInclude(hg => hg.UserDetail)
            .Include(t => t.CreatedUser).ThenInclude(e => e.UserDetail)).ToList();
            //TODO must be reverted to LoadWith LinqToDB extension method
        }

        /// <summary>
        /// Returns a list of post as dto by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<PostListDto> GetUserPostsWithDto(int userId)
        {
            IEnumerable<PostListDto> data = _postRepository.GetList(y => y.CreatedBy == userId, x => x.Include(y => y.PostImages)
              .Include(a => a.PostVideos).Include(comment => comment.PostComments)
              .ThenInclude(tr => tr.CreatedUser).ThenInclude(hg => hg.UserDetail)
              .Include(s => s.CreatedUser).ThenInclude(t => t.UserDetail))
              .Select(p => new PostListDto
              {
                  Id = p.Id,
                  Text = p.Text,
                  CreatedDate = p.CreatedDate,
                  ImageUrl = p.PostImages.Count() == 0 ? "" : p.PostImages.FirstOrDefault().ImageUrl, //TODO It can be more photos of the post
                  VideoUrl = p.PostVideos.Count() == 0 ? "" : p.PostVideos.FirstOrDefault().VideoUrl, //TODO It can be more videos of the post
                  CreatedByUserName = p.CreatedUser == null ? "" : p.CreatedUser.UserName,
                  CreatedByUserPhoto = p.CreatedUser == null ? "" : p.CreatedUser.UserDetail.ProfilePhotoPath,
                  PostType = p.PostType,
                  Comments = p.PostComments.ToList().Select(y => new PostCommentListDto
                  {
                      Text = y.Text,
                      CreatedDate = y.CreatedDate,
                      Id = y.Id,
                      CreatedByUserName = y.CreatedUser.UserName,
                      CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                      PostId = y.PostId
                  }).ToList()

                  //TODO must be reverted to LoadWith LinqToDB extension method
              }).OrderByDescending(sa => sa.CreatedDate).AsEnumerable();

            return data;
        }

        #endregion
    }
}
