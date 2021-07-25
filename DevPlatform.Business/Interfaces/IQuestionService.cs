using DevPlatform.Core.Domain.Question;
using DevPlatform.Domain.Api.QuestionApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.QuestionDto;
using DevPlatform.Domain.ServiceResponseModels.QuestionService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Question service implementations
    /// </summary>
    public partial interface IQuestionService
    {
        /// <summary>
        /// Inserts a question
        /// </summary>
        /// <param name="question"></param>
        Task<ResultModel> CreateAsync(Question question);

        /// <summary>
        /// Inserts questions by using bulk
        /// </summary>
        /// <param name="question"></param>
        Task<ResultModel> CreateAsync(List<Question> questions);

        /// <summary>
        /// Inserts posts and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> CreateAsync(QuestionCreateApi model);

        /// <summary>
        /// Deletes a question
        /// </summary>
        /// <param name="question"></param>
        Task DeleteAsync(Question question);

        /// <summary>
        /// Updates a question
        /// </summary>
        /// <param name="question"></param>
        Task UpdateAsync(Question question);

        /// <summary>
        /// Gets a question by id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<Question> GetByIdAsync(int questionId);

        /// <summary>
        /// Returns a question as Dto by QuestionId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<QuestionListDto> GetByIdAsDtoAsync(int id);

        /// <summary>
        /// Returns the question lists
        /// </summary>
        /// <returns></returns>
        Task<List<QuestionListDto>> GetQuestionListAsync();

        /// <summary>
        /// Returns questions of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<QuestionListDto>> GetUserQuestionsByUserIdAsync(int userId);

        /// <summary>
        /// Returns posts of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<QuestionListDto>> GetUserQuestionsWithDtoAsync(int userId);
    }
}
