using DevPlatform.Core.Domain.Question;
using DevPlatform.Domain.Api.QuestionApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.ServiceResponseModels.QuestionCommentService;
using System.Collections.Generic;

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
        ResultModel Create(QuestionComment comment);

        /// <summary>
        /// Deletes a question comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        ResultModel Delete(QuestionComment comment);

        /// <summary>
        /// Updates a question comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        ResultModel Update(QuestionComment comment);

        /// <summary>
        /// Return question comment by comment id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        QuestionComment GetById(int id);

        /// <summary>
        /// Returns question comment list by questionId
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        List<QuestionComment> GetQuestionCommentsByQuestionId(int questionId);

        /// <summary>
        /// Creates a question comment and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResponse<CreateResponse> Create(QuestionCommentCreateApi model);
    }
}
