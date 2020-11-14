using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Story;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.StoryDto;
using DevPlatform.Repository.Generic;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Story service
    /// </summary>
    public partial class StoryService : IStoryService
    {
        #region Fields
        private readonly IRepository<Story> _storyRepository;

        #endregion

        #region Ctor
        public StoryService(IRepository<Story> storyRepository)
        {
            _storyRepository = storyRepository;
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
            Story getStory = _storyRepository.Table.
                LoadWith(x => x.StoryImages)
                .LoadWith(x => x.StoryVideos)
                .LoadWith(x => x.StoryComments).ThenLoad(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
                .LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail).FirstOrDefault(y => y.Id == id);

            StoryListDto storyListDto = new StoryListDto
            {
                Id = getStory.Id,
                Title = getStory.Title,
                Description = getStory.Description,
                CreatedDate = getStory.CreatedDate,
                ImageUrl = getStory.StoryImages.Count() == 0 ? "" : getStory.StoryImages.FirstOrDefault().ImageUrl, //TODO It can be more photos of the story
                VideoUrl = getStory.StoryVideos.Count() == 0 ? "" : getStory.StoryVideos.FirstOrDefault().VideoUrl, //TODO It can be more videos of the story
                CreatedByUserName = getStory.CreatedUser == null ? "" : getStory.CreatedUser.UserName,
                CreatedByUserPhoto = getStory.CreatedUser.UserDetail.ProfilePhotoPath,
                StoryType = getStory.StoryType,
                Comments = getStory.StoryComments.ToList().Select(y => new StoryCommentListDto
                {
                    Text = y.Text,
                    CreatedDate = y.CreatedDate,
                    Id = y.Id,
                    CreatedByUserName = y.CreatedUser.UserName,
                    CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                    StoryId = y.StoryId
                }).ToList()
            };

            return storyListDto;
            //TODO must be reverted to LoadWith LinqToDB extension method
        }

        /// <summary>
        /// Returns a list of stories as dto
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StoryListDto> GetStoryList()
        {
            IEnumerable<StoryListDto> data = _storyRepository.Table.LoadWith(x => x.StoryImages)
              .LoadWith(x => x.StoryVideos).LoadWith(x => x.StoryComments).ThenLoad(x => x.CreatedUser)
              .ThenLoad(x => x.UserDetail).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
                .Select(p => new StoryListDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CreatedDate = p.CreatedDate,
                    ImageUrl = p.StoryImages.Count() == 0 ? "" : p.StoryImages.FirstOrDefault().ImageUrl, //TODO It can be more photos of the story
                    VideoUrl = p.StoryVideos.Count() == 0 ? "" : p.StoryVideos.FirstOrDefault().VideoUrl, //TODO It can be more videos of the story
                    CreatedByUserName = p.CreatedUser == null ? "" : p.CreatedUser.UserName,
                    CreatedByUserPhoto = p.CreatedUser == null ? "" : p.CreatedUser.UserDetail.ProfilePhotoPath,
                    StoryType = p.StoryType,
                    Comments = p.StoryComments.ToList().Select(y => new StoryCommentListDto
                    {
                        Text = y.Text,
                        CreatedDate = y.CreatedDate,
                        Id = y.Id,
                        CreatedByUserName = y.CreatedUser.UserName,
                        CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                        StoryId = y.StoryId
                    }).ToList()

                    //TODO must be reverted to LoadWith LinqToDB extension method
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
            IEnumerable<StoryListDto> getStory = _storyRepository.Table.LoadWith(x => x.StoryImages)
             .LoadWith(x => x.StoryVideos).LoadWith(x => x.StoryComments).ThenLoad(x => x.CreatedUser)
             .ThenLoad(x => x.UserDetail).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
             .Where(y => y.CreatedBy == userId)
              .Select(p => new StoryListDto
              {
                  Id = p.Id,
                  Title = p.Title,
                  Description = p.Description,
                  CreatedDate = p.CreatedDate,
                  ImageUrl = p.StoryImages.Count() == 0 ? "" : p.StoryImages.FirstOrDefault().ImageUrl, //TODO It can be more photos of the story
                  VideoUrl = p.StoryVideos.Count() == 0 ? "" : p.StoryVideos.FirstOrDefault().VideoUrl, //TODO It can be more videos of the story
                  CreatedByUserName = p.CreatedUser == null ? "" : p.CreatedUser.UserName,
                  CreatedByUserPhoto = p.CreatedUser == null ? "" : p.CreatedUser.UserDetail.ProfilePhotoPath,
                  StoryType = p.StoryType,
                  Comments = p.StoryComments.ToList().Select(y => new StoryCommentListDto
                  {
                      Text = y.Text,
                      CreatedDate = y.CreatedDate,
                      Id = y.Id,
                      CreatedByUserName = y.CreatedUser.UserName,
                      CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                      StoryId = y.StoryId
                  }).ToList()

                  //TODO must be reverted to LoadWith LinqToDB extension method
              }).OrderByDescending(sa => sa.CreatedDate).AsEnumerable();

            return getStory;
        }

        #endregion
    }
}
