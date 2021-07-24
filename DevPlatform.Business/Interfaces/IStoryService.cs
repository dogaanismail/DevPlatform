using DevPlatform.Core.Domain.Story;
using DevPlatform.Domain.Api.StoryApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.StoryDto;
using DevPlatform.Domain.ServiceResponseModels.StoryService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces
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
        Task<ResultModel> Create(Story story);

        /// <summary>
        /// Inserts stories by using bulk
        /// </summary>
        /// <param name="stories"></param>
        Task<ResultModel> Create(List<Story> stories);

        /// <summary>
        /// Deletes a story
        /// </summary>
        /// <param name="story"></param>
        Task Delete(Story story);

        /// <summary>
        /// Updates a story
        /// </summary>
        /// <param name="story"></param>
        Task Update(Story story);

        /// <summary>
        /// Gets a story by id
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        Task<Story> GetById(int storyId);

        /// <summary>
        /// Returns a story as Dto by StoryId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StoryListDto> GetByIdAsDto(int id);

        /// <summary>
        /// Returns the story lists
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<StoryListDto>> GetStoryList();

        /// <summary>
        /// Returns stories of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Story>> GetUserStoriesByUserId(int userId);

        /// <summary>
        /// Returns stories of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<StoryListDto>> GetUserStoriesWithDto(int userId);

        /// <summary>
        /// Inserts stories and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> Create(StoryCreateApi model);

        /// <summary>
        /// Inserts stories for post and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> Create(StoryCreateApi model, bool isCreateWithPost = true);
    }
}
