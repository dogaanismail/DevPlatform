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
        Task<ResultModel> Create(Question question);

        /// <summary>
        /// Inserts questions by using bulk
        /// </summary>
        /// <param name="question"></param>
        Task<ResultModel> Create(List<Question> questions);

        /// <summary>
        /// Deletes a question
        /// </summary>
        /// <param name="question"></param>
        Task Delete(Question question);

        /// <summary>
        /// Updates a question
        /// </summary>
        /// <param name="question"></param>
        Task Update(Question question);

        /// <summary>
        /// Gets a question by id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<Question> GetById(int questionId);

        /// <summary>
        /// Returns a question as Dto by QuestionId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<QuestionListDto> GetByIdAsDto(int id);

        /// <summary>
        /// Returns the question lists
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<QuestionListDto>> GetQuestionList();

        /// <summary>
        /// Returns questions of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<QuestionListDto>> GetUserQuestionsByUserId(int userId);

        /// <summary>
        /// Returns posts of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<QuestionListDto>> GetUserQuestionsWithDto(int userId);

        /// <summary>
        /// Inserts posts and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> Create(QuestionCreateApi model);
    }
}
