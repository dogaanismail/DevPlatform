using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Repository.Generic;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using DevPlatform.Domain.Api;
using DevPlatform.Common.Helpers;
using CloudinaryDotNet.Actions;
using DevPlatform.Domain.Enumerations;
using System.Net;
using Microsoft.AspNetCore.Http;
using DevPlatform.Domain.ServiceResponseModels.PostService;
using Newtonsoft.Json;
using DevPlatform.Domain.Api.StoryApi;
using StoryCreateResponse = DevPlatform.Domain.ServiceResponseModels.StoryService.CreateResponse;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Post service
    /// </summary>
    public partial class PostService : ServiceExecute, IPostService
    {
        #region Fields
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppUserDetail> _userDetailRepository;
        private readonly IUserService _userService;
        private readonly IPostImageService _postImageService;
        private readonly IPostVideoService _postVideoService;
        private readonly IImageProcessingService _imageProcessingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;
        private readonly IStoryService _storyService;

        #endregion

        #region Ctor
        public PostService(IRepository<Post> postRepository,
            IRepository<AppUser> userRepository,
            IRepository<AppUserDetail> userDetailRepository,
            IUserService userService,
            IPostImageService postImageService,
            IPostVideoService postVideoService,
            IHttpContextAccessor httpContextAccessor,
            IImageProcessingService imageProcessingService,
            ILogService logService,
            IStoryService storyService)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _userDetailRepository = userDetailRepository;
            _userService = userService;
            _postImageService = postImageService;
            _postVideoService = postVideoService;
            _imageProcessingService = imageProcessingService;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
            _storyService = storyService;
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
        /// Inserts posts by using bulk
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        public ResultModel Create(List<Post> posts)
        {
            if (posts == null)
                throw new ArgumentNullException(nameof(posts));

            _postRepository.Insert(posts);
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
            Post getPost = _postRepository.Table.
                LoadWith(x => x.PostImages)
                .LoadWith(x => x.PostVideos)
                .LoadWith(x => x.PostComments).ThenLoad(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
                .LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail).FirstOrDefault(y => y.Id == id);

            PostListDto postListDto = new PostListDto
            {
                Id = getPost.Id,
                Text = getPost.Text,
                CreatedDate = getPost.CreatedDate,
                ImageUrlList = getPost.PostImages.Count > 0 ? getPost.PostImages.Select(x => x.ImageUrl).ToList() : new List<string>(),
                VideoUrl = getPost.PostVideos.Count() == 0 ? "" : getPost.PostVideos.FirstOrDefault().VideoUrl,
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
            IEnumerable<PostListDto> data = _postRepository.Table
                .LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
                .LoadWith(x => x.PostImages).LoadWith(x => x.PostVideos)
                .LoadWith(x => x.PostComments).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
                .Select(p => new PostListDto
                {
                    Id = p.Id,
                    Text = p.Text,
                    CreatedDate = p.CreatedDate,
                    ImageUrlList = p.PostImages.Count > 0 ? p.PostImages.Select(x => x.ImageUrl).ToList() : new List<string>(),
                    VideoUrl = p.PostVideos.Count() == 0 ? "" : p.PostVideos.FirstOrDefault().VideoUrl,
                    CreatedByUserName = p.CreatedUser == null ? "" : p.CreatedUser.UserName,
                    CreatedByUserPhoto = p.CreatedUser == null ? "" : p.CreatedUser.UserDetail.ProfilePhotoPath,
                    PostType = p.PostType,
                    FancyboxData = $"post{p.Id}",
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
            IEnumerable<Post> getPost = _postRepository.Table.LoadWith(x => x.PostImages)
              .LoadWith(x => x.PostVideos).LoadWith(x => x.PostComments).ThenLoad(x => x.CreatedUser)
              .ThenLoad(x => x.UserDetail).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
              .Where(y => y.CreatedBy == userId).ToList();

            return getPost;
        }

        /// <summary>
        /// Returns a list of post as dto by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<PostListDto> GetUserPostsWithDto(int userId)
        {
            IEnumerable<PostListDto> getPost = _postRepository.Table.LoadWith(x => x.PostImages)
             .LoadWith(x => x.PostVideos).LoadWith(x => x.PostComments).ThenLoad(x => x.CreatedUser)
             .ThenLoad(x => x.UserDetail).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
             .Where(y => y.CreatedBy == userId)
              .Select(p => new PostListDto
              {
                  Id = p.Id,
                  Text = p.Text,
                  CreatedDate = p.CreatedDate,
                  ImageUrlList = p.PostImages.Count > 0 ? p.PostImages.Select(x => x.ImageUrl).ToList() : new List<string>(),
                  VideoUrl = p.PostVideos.Count() == 0 ? "" : p.PostVideos.FirstOrDefault().VideoUrl,
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

            return getPost;
        }

        /// <summary>
        /// Inserts posts and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResponse<CreateResponse> Create(PostCreateApi model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var serviceResponse = new ServiceResponse<CreateResponse>
            {
                Success = false
            };

            try
            {
                if (string.IsNullOrEmpty(model.Text))
                    return ServiceResponse((CreateResponse)null, new List<string> { "Text can not be null !" });

                var appUser = _userService.FindByUserName(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (appUser == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "User not found!" });

                #region CloudinaryProcess

                var imageUploadResult = new List<ImageUploadResult>();
                var videoUploadResult = new VideoUploadResult();

                bool hasImage = CheckItemType.HasItemImage(model);
                bool hasVideo = CheckItemType.HasItemVideo(model);

                #region ImageUploadingProcess
                if (hasImage)
                {
                    imageUploadResult = _imageProcessingService.UploadImage(model.Images);

                    if (imageUploadResult.Any(x => x.Error != null))
                        return ServiceResponse((CreateResponse)null, new List<string> { string.Join(Environment.NewLine, imageUploadResult.Select(err => string.Join(Environment.NewLine, err.Error.Message))) });
                }

                #endregion

                #region VideoUploadingProcess

                if (hasVideo)
                {
                    videoUploadResult = _imageProcessingService.UploadVideo(model.Video);

                    if (videoUploadResult.Error != null)
                        return ServiceResponse((CreateResponse)null, new List<string> { videoUploadResult.Error.Message.ToString() });
                }

                #endregion

                #endregion

                #region POST CRUD

                var newPost = new Post
                {
                    Text = model.Text,
                    PostType = GetPostType(hasImage, hasVideo),
                    CreatedBy = appUser.Id
                };

                #region PostImages
                if (imageUploadResult != null && imageUploadResult.Count > 0)
                {
                    foreach (var uploadedImage in imageUploadResult)
                    {
                        newPost.PostImages.Add(new PostImage { ImageUrl = uploadedImage.Url.ToString(), CreatedBy = appUser.Id });
                    }
                }

                #endregion

                #region PostVideos

                if (videoUploadResult != null && videoUploadResult.StatusCode == HttpStatusCode.OK)
                    newPost.PostVideos.Add(new PostVideo { VideoUrl = videoUploadResult.Url.ToString() });

                #endregion

                ResultModel postModel = Create(newPost);

                if (!postModel.Status)
                    return ServiceResponse((CreateResponse)null, new List<string> { postModel.Message });

                #endregion

                #region Story CRUD

                StoryCreateResponse storyCreateResponse = null;

                if (model.IsStory.GetValueOrDefault() && (model.Images != null || model.Video != null))
                {
                    var storyCreateModel = new StoryCreateApi()
                    {
                        Title = model.Text,
                        Description = model.Text,
                        PhotoUrl = imageUploadResult?.Select(x => x.Url.ToString()).FirstOrDefault(),
                        VideoUrl = videoUploadResult?.Url?.ToString()
                    };

                    _logService.InsertLogAsync(LogLevel.Information, $"PostService - Create Story Request", JsonConvert.SerializeObject(storyCreateModel));

                    var storyServiceResponse = _storyService.Create(storyCreateModel, isCreateWithPost: true);

                    if (storyServiceResponse.Warnings.Any())
                    {
                        _logService.InsertLogAsync(LogLevel.Information, $"PostService - Create Story Error Response ", JsonConvert.SerializeObject(storyServiceResponse));

                        //TODO : Story create error process must be handled!
                    }

                    storyCreateResponse = storyServiceResponse.Data;
                }

                #endregion

                serviceResponse.Success = true;
                serviceResponse.ResultCode = ResultCode.Success;
                serviceResponse.Data = new CreateResponse
                {
                    Id = newPost.Id,
                    Text = newPost.Text,
                    ImageUrlList = imageUploadResult?.Select(x => x.Url.ToString()).ToList(),
                    CreatedByUserName = appUser?.UserName,
                    CreatedByUserPhoto = appUser?.UserDetail.ProfilePhotoPath,
                    CreatedDate = newPost.CreatedDate,
                    VideoUrl = videoUploadResult?.Url?.ToString(),
                    PostType = newPost.PostType,
                    Comments = null,
                    StoryCreateResponse = storyCreateResponse
                };

                return serviceResponse;
            }
            catch (Exception ex)
            {
                _logService.InsertLogAsync(LogLevel.Error, $"PostService- Create Error: model {JsonConvert.SerializeObject(model)}", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.ResultCode = ResultCode.Exception;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns PostType by PostTypeId
        /// </summary>
        /// <param name="hasImage"></param>
        /// <param name="hasVideo"></param>
        /// <returns></returns>
        private static int GetPostType(bool hasImage, bool hasVideo)
        {
            return hasImage == true ? (int)PostTypeEnum.PostImage
                : hasVideo == true ? (int)PostTypeEnum.PostVideo
                : (int)PostTypeEnum.PostText;
        }

        #endregion

    }
}
