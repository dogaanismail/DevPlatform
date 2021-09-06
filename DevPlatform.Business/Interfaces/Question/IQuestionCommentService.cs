using DevPlatform.Core.Domain.Question;
using DevPlatform.Domain.Api.QuestionApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.ServiceResponseModels.QuestionCommentService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces.Question
{
    /// <summary>
    /// Question comment service interface implementations
    /// </summary>
    public partial interface IQuestionCommentService
    {
        /// <summary>
        /// Creates question comments for a question.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<ResultModel> CreateAsync(QuestionComment comment);

        /// <summary>
        /// Creates a question comment and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> CreateAsync(QuestionCommentCreateApi model);

        /// <summary>
        /// Deletes a question comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteAsync(QuestionComment comment);

        /// <summary>
        /// Updates a question comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateAsync(QuestionComment comment);

        /// <summary>
        /// Return question comment by comment id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<QuestionComment> GetByIdAsync(int id);

        /// <summary>
        /// Returns question comment list by questionId
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<List<QuestionComment>> GetQuestionCommentsByQuestionIdAsync(int questionId);
    }
}
