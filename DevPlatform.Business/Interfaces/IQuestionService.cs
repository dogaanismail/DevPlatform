using DevPlatform.Core.Domain.Question;
using DevPlatform.Domain.Api.QuestionApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.QuestionDto;
using DevPlatform.Domain.ServiceResponseModels.QuestionService;
using System.Collections.Generic;

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
        ResultModel Create(Question question);

        /// <summary>
        /// Inserts questions by using bulk
        /// </summary>
        /// <param name="question"></param>
        ResultModel Create(List<Question> questions);

        /// <summary>
        /// Deletes a question
        /// </summary>
        /// <param name="question"></param>
        void Delete(Question question);

        /// <summary>
        /// Updates a question
        /// </summary>
        /// <param name="question"></param>
        void Update(Question question);

        /// <summary>
        /// Gets a question by id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Question GetById(int questionId);

        /// <summary>
        /// Returns a question as Dto by QuestionId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        QuestionListDto GetByIdAsDto(int id);

        /// <summary>
        /// Returns the question lists
        /// </summary>
        /// <returns></returns>
        IEnumerable<QuestionListDto> GetQuestionList();

        /// <summary>
        /// Returns questions of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Question> GetUserQuestionsByUserId(int userId);

        /// <summary>
        /// Returns posts of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<QuestionListDto> GetUserQuestionsWithDto(int userId);

        /// <summary>
        /// Inserts posts and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResponse<CreateResponse> Create(QuestionCreateApi model);
    }
}
