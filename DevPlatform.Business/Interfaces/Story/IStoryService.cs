using StoryEntity = DevPlatform.Core.Domain.Story.Story;
using DevPlatform.Domain.Api.StoryApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.StoryDto;
using DevPlatform.Domain.ServiceResponseModels.StoryService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces.Story
{
    /// <summary>
    /// Story service interface
    /// </summary>
    public interface IStoryService
    {
        /// <summary>
        /// Inserts a story
        /// </summary>
        /// <param name="story"></param>
        Task<ResultModel> CreateAsync(StoryEntity story);

        /// <summary>
        /// Inserts stories by using bulk
        /// </summary>
        /// <param name="stories"></param>
        Task<ResultModel> CreateAsync(List<StoryEntity> stories);

        /// <summary>
        /// Inserts stories and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> CreateAsync(StoryCreateApi model);

        /// <summary>
        /// Inserts stories for post and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> CreateAsync(StoryCreateApi model, bool isCreateWithPost = true);

        /// <summary>
        /// Deletes a story
        /// </summary>
        /// <param name="story"></param>
        Task DeleteAsync(StoryEntity story);

        /// <summary>
        /// Updates a story
        /// </summary>
        /// <param name="story"></param>
        Task UpdateAsync(StoryEntity story);

        /// <summary>
        /// Gets a story by id
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        Task<StoryEntity> GetByIdAsync(int storyId);

        /// <summary>
        /// Returns a story as Dto by StoryId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StoryListDto> GetByIdAsDtoAsync(int id);

        /// <summary>
        /// Returns the story lists
        /// </summary>
        /// <returns></returns>
        Task<List<StoryListDto>> GetStoryListAsync();

        /// <summary>
        /// Returns stories of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<StoryEntity>> GetUserStoriesByUserIdAsync(int userId);

        /// <summary>
        /// Returns stories of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<StoryListDto>> GetUserStoriesWithDtoAsync(int userId);
    }
}
