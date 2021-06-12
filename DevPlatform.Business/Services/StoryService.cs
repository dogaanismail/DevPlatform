using CloudinaryDotNet.Actions;
using DevPlatform.Business.Interfaces;
using DevPlatform.Common.Helpers;
using DevPlatform.Core.Domain.Story;
using DevPlatform.Domain.Api.StoryApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.StoryDto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Domain.ServiceResponseModels.StoryService;
using DevPlatform.Repository.Generic;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Story service
    /// </summary>
    public partial class StoryService : ServiceExecute, IStoryService
    {
        #region Fields
        private readonly IRepository<Story> _storyRepository;
        private readonly ILogService _logService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IImageProcessingService _imageProcessingService;
        private readonly IStoryImageService _storyImageService;
        private readonly IStoryVideoService _storyVideoService;

        #endregion

        #region Ctor
        public StoryService(IRepository<Story> storyRepository,
            ILogService logService,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IImageProcessingService imageProcessingService,
            IStoryImageService storyImageService,
            IStoryVideoService storyVideoService)
        {
            _storyRepository = storyRepository;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _imageProcessingService = imageProcessingService;
            _storyImageService = storyImageService;
            _storyVideoService = storyVideoService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Deletes a story
        /// </summary>
        /// <param name="story"></param>
        public virtual void Delete(Story story)
        {
            if (story == null)
                throw new ArgumentNullException(nameof(story));

            _storyRepository.Delete(story);
        }

        /// <summary>
        /// Gets a story by id
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        public virtual Story GetById(int storyId)
        {
            if (storyId == 0)
                return null;

            return _storyRepository.GetById(storyId);
        }

        /// <summary>
        /// Inserts a story
        /// </summary>
        /// <param name="story"></param>
        public virtual ResultModel Create(Story story)
        {
            if (story == null)
                throw new ArgumentNullException(nameof(story));

            _storyRepository.Insert(story);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Inserts stories by using bulk
        /// </summary>
        /// <param name="stories"></param>
        /// <returns></returns>
        public ResultModel Create(List<Story> stories)
        {
            if (stories == null)
                throw new ArgumentNullException(nameof(stories));

            _storyRepository.Insert(stories);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Updates a story
        /// </summary>
        /// <param name="story"></param>
        public virtual void Update(Story story)
        {
            if (story == null)
                throw new ArgumentNullException(nameof(story));

            _storyRepository.Update(story);
        }

        /// <summary>
        /// Returns a story by id as dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StoryListDto GetByIdAsDto(int id)
        {
            var data = _storyRepository.Table.Where(x => x.Id == id).Select(p => new StoryListDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                ImageUrl = p.StoryImages.FirstOrDefault().ImageUrl ?? "",
                VideoUrl = p.StoryVideos.FirstOrDefault().VideoUrl ?? "",
                CreatedByUserName = p.CreatedUser.UserName ?? "",
                CreatedByUserPhoto = p.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                StoryType = p.StoryType,
                Comments = p.StoryComments.Select(y => new StoryCommentListDto
                {
                    Text = y.Text,
                    CreatedDate = y.CreatedDate,
                    Id = y.Id,
                    CreatedByUserName = y.CreatedUser.UserName ?? "",
                    CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                    StoryId = y.StoryId
                }).ToList()
            }).OrderByDescending(sa => sa.CreatedDate).FirstOrDefault();

            return data;
        }

        /// <summary>
        /// Returns a list of stories as dto
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StoryListDto> GetStoryList()
        {
            var data = _storyRepository.Table.Select(p => new StoryListDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                ImageUrl = p.StoryImages.FirstOrDefault().ImageUrl ?? "",
                VideoUrl = p.StoryVideos.FirstOrDefault().VideoUrl ?? "",
                CreatedByUserName = p.CreatedUser.UserName ?? "",
                CreatedByUserPhoto = p.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                StoryType = p.StoryType,
                Comments = p.StoryComments.Select(y => new StoryCommentListDto
                {
                    Text = y.Text,
                    CreatedDate = y.CreatedDate,
                    Id = y.Id,
                    CreatedByUserName = y.CreatedUser.UserName ?? "",
                    CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                    StoryId = y.StoryId
                }).ToList()
            }).OrderByDescending(sa => sa.CreatedDate).AsEnumerable();

            return data;
        }

        /// <summary>
        /// Returns a list of story by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Story> GetUserStoriesByUserId(int userId)
        {
            IEnumerable<Story> getStory = _storyRepository.Table.LoadWith(x => x.StoryImages)
              .LoadWith(x => x.StoryVideos).LoadWith(x => x.StoryComments).ThenLoad(x => x.CreatedUser)
              .ThenLoad(x => x.UserDetail).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
              .Where(y => y.CreatedBy == userId).ToList();

            return getStory;
        }

        /// <summary>
        /// Returns a list of story as dto by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<StoryListDto> GetUserStoriesWithDto(int userId)
        {
            var data = _storyRepository.Table.Where(x => x.CreatedBy == userId).Select(p => new StoryListDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                ImageUrl = p.StoryImages.FirstOrDefault().ImageUrl ?? "",
                VideoUrl = p.StoryVideos.FirstOrDefault().VideoUrl ?? "",
                CreatedByUserName = p.CreatedUser.UserName ?? "",
                CreatedByUserPhoto = p.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                StoryType = p.StoryType,
                Comments = p.StoryComments.Select(y => new StoryCommentListDto
                {
                    Text = y.Text,
                    CreatedDate = y.CreatedDate,
                    Id = y.Id,
                    CreatedByUserName = y.CreatedUser.UserName ?? "",
                    CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                    StoryId = y.StoryId
                }).ToList()
            }).OrderByDescending(sa => sa.CreatedDate).AsEnumerable();

            return data;
        }

        /// <summary>
        /// Inserts stories and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResponse<CreateResponse> Create(StoryCreateApi model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var serviceResponse = new ServiceResponse<CreateResponse>
            {
                Success = false
            };

            try
            {
                var appUser = _userService.FindByUserName(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (appUser == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "User not found!" });

                #region CloudinaryProcess

                var imageUploadResult = new ImageUploadResult();
                var videoUploadResult = new VideoUploadResult();

                bool hasImage = CheckItemType.HasItemImage(model);
                bool hasVideo = CheckItemType.HasItemVideo(model);


                #region ImageUploadingProcess
                if (hasImage)
                {
                    imageUploadResult = _imageProcessingService.UploadImage(model.Photo);

                    _logService.InsertLogAsync(LogLevel.Information, $"StoryService- ImageUpload response from Cloudinary", JsonConvert.SerializeObject(imageUploadResult), appUser);

                    if (imageUploadResult.Error != null)
                        return ServiceResponse((CreateResponse)null, new List<string> { imageUploadResult.Error.Message.ToString() });
                }

                #endregion

                #region VideoUploadingProcess

                if (hasVideo)
                {
                    videoUploadResult = _imageProcessingService.UploadVideo(model.Video);

                    _logService.InsertLogAsync(LogLevel.Information, $"StoryService- VideoUpload response from Cloudinary", JsonConvert.SerializeObject(imageUploadResult), appUser);

                    if (videoUploadResult.Error != null)
                        return ServiceResponse((CreateResponse)null, new List<string> { videoUploadResult.Error.Message.ToString() });
                }

                #endregion

                #endregion

                #region STORY CRUD

                var newStory = new Story
                {
                    Title = model.Title,
                    Description = model.Description,
                    StoryType = GetStoryType(hasImage, hasVideo),
                    CreatedBy = appUser.Id
                };

                ResultModel storyModel = Create(newStory);

                if (!storyModel.Status)
                    return ServiceResponse((CreateResponse)null, new List<string> { storyModel.Message });

                #region StoryImages
                if (imageUploadResult != null && imageUploadResult.StatusCode == HttpStatusCode.OK)
                {
                    var postImages = new StoryImage
                    {
                        StoryId = newStory.Id,
                        ImageUrl = imageUploadResult.Url.ToString(),
                        CreatedBy = appUser.Id
                    };

                    ResultModel storyImageModel = _storyImageService.Create(postImages);

                    if (!storyImageModel.Status)
                        return ServiceResponse((CreateResponse)null, new List<string> { storyImageModel.Message });
                }

                #endregion

                #region StoryVideos

                if (videoUploadResult != null && videoUploadResult.StatusCode == HttpStatusCode.OK)
                {
                    var postVideos = new StoryVideo
                    {
                        StoryId = newStory.Id,
                        VideoUrl = videoUploadResult.Url.ToString(),
                        CreatedBy = appUser.Id
                    };
                    ResultModel storyVideoModel = _storyVideoService.Create(postVideos);

                    if (!storyVideoModel.Status)
                        return ServiceResponse((CreateResponse)null, new List<string> { storyVideoModel.Message });
                }

                #endregion

                #endregion

                serviceResponse.Success = true;
                serviceResponse.ResultCode = ResultCode.Success;
                serviceResponse.Data = new CreateResponse
                {
                    Id = newStory.Id,
                    Title = newStory.Title,
                    Description = newStory.Description,
                    ImageUrl = imageUploadResult?.Url?.ToString(),
                    CreatedByUserName = appUser?.UserName,
                    CreatedByUserPhoto = appUser?.UserDetail.ProfilePhotoPath,
                    CreatedDate = newStory.CreatedDate,
                    VideoUrl = videoUploadResult?.Url?.ToString(),
                    StoryType = newStory.StoryType,
                };

                return serviceResponse;

            }
            catch (Exception ex)
            {
                _logService.InsertLogAsync(LogLevel.Error, $"StoryService- Create Error: model {JsonConvert.SerializeObject(model)}", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.ResultCode = ResultCode.Exception;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        /// <summary>
        /// Inserts stories for post and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual ServiceResponse<CreateResponse> Create(StoryCreateApi model, bool isCreateWithPost = true)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var serviceResponse = new ServiceResponse<CreateResponse>
            {
                Success = false
            };

            try
            {
                var appUser = _userService.FindByUserName(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (appUser == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "User not found!" });

                var storyType = GetStoryTypeWithUrl(model);

                var newStory = new Story
                {
                    Title = model.Title,
                    Description = model.Description,
                    StoryType = storyType
                };

                switch ((StoryTypeEnum)storyType)
                {
                    case StoryTypeEnum.StoryImage:
                        newStory.StoryImages.Add(new StoryImage { ImageUrl = model.PhotoUrl, CreatedBy = appUser.Id });
                        break;
                    case StoryTypeEnum.StoryVideo:
                        newStory.StoryVideos.Add(new StoryVideo { VideoUrl = model.VideoUrl, CreatedBy = appUser.Id });
                        break;
                    case StoryTypeEnum.StoryText:
                        break;
                    case StoryTypeEnum.StoryGif:
                        break;
                    default:
                        break;
                }

                ResultModel storyModel = Create(newStory);

                if (!storyModel.Status)
                    return ServiceResponse((CreateResponse)null, new List<string> { storyModel.Message });

                serviceResponse.Success = true;
                serviceResponse.ResultCode = ResultCode.Success;
                serviceResponse.Data = new CreateResponse
                {
                    Id = newStory.Id,
                    Title = newStory.Title,
                    Description = newStory.Description,
                    CreatedByUserName = appUser?.UserName,
                    CreatedByUserPhoto = appUser?.UserDetail?.ProfilePhotoPath,
                    CreatedDate = newStory.CreatedDate,
                    ImageUrl = model.PhotoUrl,
                    VideoUrl = model.VideoUrl,
                    StoryType = newStory.StoryType
                };

                return serviceResponse;
            }
            catch (Exception ex)
            {
                _logService.InsertLogAsync(LogLevel.Error, $"StoryService- CreateForPost Error: model {JsonConvert.SerializeObject(model)}", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.ResultCode = ResultCode.Exception;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns StoryType by storyCreate model
        /// </summary>
        /// <param name="hasImage"></param>
        /// <param name="hasVideo"></param>
        /// <returns></returns>
        private static int GetStoryTypeWithUrl(StoryCreateApi model)
        {
            return !string.IsNullOrEmpty(model.PhotoUrl) ? (int)StoryTypeEnum.StoryImage
                : !string.IsNullOrEmpty(model.VideoUrl) ? (int)StoryTypeEnum.StoryVideo
                : (int)StoryTypeEnum.StoryText;
        }

        /// <summary>
        /// Returns storyType by storyTypeId
        /// </summary>
        /// <param name="hasImage"></param>
        /// <param name="hasVideo"></param>
        /// <returns></returns>
        private static int GetStoryType(bool hasImage, bool hasVideo)
        {
            return hasImage == true ? (int)StoryTypeEnum.StoryImage
                : hasVideo == true ? (int)StoryTypeEnum.StoryVideo
                : (int)StoryTypeEnum.StoryText;
        }

        #endregion
    }
}
