﻿using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Question;
using DevPlatform.Domain.Api.QuestionApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Domain.ServiceResponseModels.QuestionCommentService;
using DevPlatform.Repository.Generic;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Question comment service implementations
    /// </summary>
    public partial class QuestionCommentService : ServiceExecute, IQuestionCommentService
    {
        #region Fields
        private readonly IRepository<QuestionComment> _questionCommentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly ILogService _logService;
        private readonly IUserService _userService;

        #endregion

        #region Ctor

        public QuestionCommentService(IRepository<QuestionComment> questionCommentRepository,
            IHttpContextAccessor httpContextAccessor,
            IRepository<Question> questionRepository,
            IRepository<AppUser> userRepository,
            ILogService logService,
            IUserService userService)
        {
            _questionCommentRepository = questionCommentRepository;
            _httpContextAccessor = httpContextAccessor;
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _logService = logService;
            _userService = userService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a question comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateAsync(QuestionComment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            await _questionCommentRepository.InsertAsync(comment);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Deletes a question comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> DeleteAsync(QuestionComment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            await _questionCommentRepository.DeleteAsync(comment);
            return new ResultModel { Status = true, Message = "Delete Process Success ! " };
        }

        /// <summary>
        /// Updates a post comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> UpdateAsync(QuestionComment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            await _questionCommentRepository.UpdateAsync(comment);
            return new ResultModel { Status = true, Message = "Update Process Success ! " };
        }

        /// <summary>
        /// Returns a question comment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<QuestionComment> GetByIdAsync(int id)
        {
            return await _questionCommentRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Returns a list of question comment by questionId
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public virtual async Task<List<QuestionComment>> GetQuestionCommentsByQuestionIdAsync(int questionId)
        {
            return await _questionCommentRepository.Table.Where(y => y.QuestionId == questionId)
                .LoadWith(x => x.CreatedUser)
                .ThenLoad(x => x.UserDetail)
                .ToListAsyncMethod();
        }

        /// <summary>
        /// Creates a question comment and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task <ServiceResponse<CreateResponse>> CreateAsync(QuestionCommentCreateApi model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var serviceResponse = new ServiceResponse<CreateResponse>
            {
                Success = false
            };

            try
            {
                if (model.QuestionId == 0)
                    return ServiceResponse((CreateResponse)null, new List<string> { "QuestionId can not be null !" });

                if (string.IsNullOrEmpty(model.Text))
                    return ServiceResponse((CreateResponse)null, new List<string> { "Text can not be null !" });

                var commentQuestion = _questionRepository.Table.FirstOrDefault(p => p.Id == model.QuestionId);

                if (commentQuestion == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "Question not found!" });

                var appUser = await _userService.FindByUserNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

                if (appUser == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "User not found!" });

                QuestionComment newComment = new()
                {
                    QuestionId = commentQuestion.Id,
                    Text = model.Text,
                    CreatedBy = appUser.Id
                };

                ResultModel result = await CreateAsync(newComment);

                if (!result.Status)
                    return ServiceResponse((CreateResponse)null, new List<string> { result.Message });

                serviceResponse.Success = true;
                serviceResponse.Data = new CreateResponse
                {
                    Text = newComment.Text,
                    Id = newComment.Id,
                    QuestionId = newComment.QuestionId,
                    CreatedDate = newComment.CreatedDate,
                    CreatedByUserName = appUser.UserName,
                    CreatedByUserPhoto = appUser.UserDetail?.ProfilePhotoPath
                };

                return serviceResponse;

            }
            catch (Exception ex)
            {
                await _logService.InsertLogAsync(LogLevel.Error, $"QuestionCommentService- Create Error: model {JsonConvert.SerializeObject(model)}", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        #endregion
    }
}
