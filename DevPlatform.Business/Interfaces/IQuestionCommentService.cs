using DevPlatform.Core.Domain.Question;
using DevPlatform.Domain.Api.QuestionApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.ServiceResponseModels.QuestionCommentService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces
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
        Task<ResultModel> Create(QuestionComment comment);

        /// <summary>
        /// Deletes a question comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<ResultModel> Delete(QuestionComment comment);

        /// <summary>
        /// Updates a question comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<ResultModel> Update(QuestionComment comment);

        /// <summary>
        /// Return question comment by comment id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<QuestionComment> GetById(int id);

        /// <summary>
        /// Returns question comment list by questionId
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<List<QuestionComment>> GetQuestionCommentsByQuestionId(int questionId);

        /// <summary>
        /// Creates a question comment and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> Create(QuestionCommentCreateApi model);
    }
}
