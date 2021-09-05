﻿using DevPlatform.Business.Common.CacheKeys.Question;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Caching;
using DevPlatform.Core.Domain.Question;
using DevPlatform.Domain.Api.QuestionApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.QuestionDto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Domain.ServiceResponseModels.QuestionService;
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
    /// Question service implementations
    /// </summary>
    public partial class QuestionService : ServiceExecute, IQuestionService
    {
        #region Fields
        private readonly IRepository<Question> _questionRepository;
        private readonly IUserService _userService;
        private readonly ILogService _logService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStaticCacheManager _staticCacheManager;

        #endregion

        #region Ctor

        public QuestionService(IRepository<Question> questionRepository,
            IUserService userService,
            ILogService logService,
            IHttpContextAccessor httpContextAccessor,
            IStaticCacheManager staticCacheManager)
        {
            _questionRepository = questionRepository;
            _userService = userService;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
            _staticCacheManager = staticCacheManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateAsync(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            await _questionRepository.InsertAsync(question);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        ///  Inserts questions by using bulk
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateAsync(List<Question> questions)
        {
            if (questions == null)
                throw new ArgumentNullException(nameof(questions));

            await _questionRepository.InsertAsync(questions);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Inserts questions and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<ServiceResponse<CreateResponse>> CreateAsync(QuestionCreateApi model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var serviceResponse = new ServiceResponse<CreateResponse>
            {
                Success = false
            };

            try
            {
                if (string.IsNullOrEmpty(model.Title))
                    return ServiceResponse((CreateResponse)null, new List<string> { "Title can not be null !" });

                var appUser = await _userService.FindByUserNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (appUser == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "User not found!" });

                #region Question crud

                var newQuestion = new Question
                {
                    Title = model.Title,
                    Description = model.Description,
                    Tags = string.Join(",", model.Tags.Select(err => string.Join(",", err.Value))),
                    CreatedBy = appUser.Id
                };

                ResultModel questionModel = await CreateAsync(newQuestion);

                if (!questionModel.Status)
                    return ServiceResponse((CreateResponse)null, new List<string> { questionModel.Message });
                #endregion

                serviceResponse.Success = true;
                serviceResponse.ResultCode = ResultCode.Success;
                serviceResponse.Data = new CreateResponse
                {
                    Id = newQuestion.Id,
                    Title = newQuestion.Title,
                    Description = newQuestion.Description,
                    Tags = newQuestion.Tags.Split(',', StringSplitOptions.None).ToList(),
                    CreatedByUserName = appUser?.UserName,
                    CreatedByUserPhoto = appUser?.UserDetail.ProfilePhotoPath,
                    CreatedDate = newQuestion.CreatedDate,
                    Comments = null
                };

                return serviceResponse;

            }
            catch (Exception ex)
            {
                await _logService.InsertLogAsync(LogLevel.Error, $"QuestionService- Create Error: model {JsonConvert.SerializeObject(model)}", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.ResultCode = ResultCode.Exception;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        /// <summary>
        /// Deletes q question
        /// </summary>
        /// <param name="question"></param>
        public virtual async Task DeleteAsync(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            await _questionRepository.DeleteAsync(question);
        }

        /// <summary>
        /// Updates a question
        /// </summary>
        /// <param name="question"></param>
        public virtual async Task UpdateAsync(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            await _questionRepository.UpdateAsync(question);
        }

        /// <summary>
        /// Gets a question by id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public virtual async Task<Question> GetByIdAsync(int questionId)
        {
            if (questionId == 0)
                return null;

            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(DevPlatformEntityCacheDefaults<Question>.ByIdCacheKey, questionId);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                return await _questionRepository.GetByIdAsync(questionId);
            });
        }

        /// <summary>
        /// Returns a question by id as dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<QuestionListDto> GetByIdAsDtoAsync(int id)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(DevPlatformEntityCacheDefaults<Question>.ByIdCacheKey, id);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                return await _questionRepository.Table.Where(x => x.Id == id).Select(p => new QuestionListDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CreatedDate = p.CreatedDate,
                    CreatedByUserName = p.CreatedUser.UserName ?? "",
                    CreatedByUserPhoto = p.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                    Tags = p.Tags.Split(',', StringSplitOptions.None).ToList(),
                    Comments = p.QuestionComments.Select(y => new QuestionCommentListDto
                    {
                        Text = y.Text,
                        CreatedDate = y.CreatedDate,
                        Id = y.Id,
                        CreatedByUserName = y.CreatedUser.UserName ?? "",
                        CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                        QuestionId = y.QuestionId
                    }).ToList()
                }).FirstOrDefaultAsyncMethod();
            });
        }

        /// <summary>
        /// Returns a list of questions as dto
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<QuestionListDto>> GetQuestionListAsync()
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(QuestionCacheKeys.QuestionsAllCacheKey);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                return await _questionRepository.Table.Select(p => new QuestionListDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CreatedDate = p.CreatedDate,
                    CreatedByUserName = p.CreatedUser.UserName ?? "",
                    CreatedByUserPhoto = p.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                    Tags = p.Tags.Split(',', StringSplitOptions.None).ToList(),
                    Comments = p.QuestionComments.Select(y => new QuestionCommentListDto
                    {
                        Text = y.Text,
                        CreatedDate = y.CreatedDate,
                        Id = y.Id,
                        CreatedByUserName = y.CreatedUser.UserName ?? "",
                        CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                        QuestionId = y.QuestionId
                    }).ToList()
                }).ToListAsyncMethod();
            });
        }

        /// <summary>
        /// Returns a list of question by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<List<QuestionListDto>> GetUserQuestionsByUserIdAsync(int userId)
        {
            return await _questionRepository.Table.Where(que => que.CreatedBy == userId).Select(p => new QuestionListDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                CreatedByUserName = p.CreatedUser.UserName ?? "",
                CreatedByUserPhoto = p.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                Tags = p.Tags.Split(',', StringSplitOptions.None).ToList(),
                Comments = p.QuestionComments.Select(y => new QuestionCommentListDto
                {
                    Text = y.Text,
                    CreatedDate = y.CreatedDate,
                    Id = y.Id,
                    CreatedByUserName = y.CreatedUser.UserName ?? "",
                    CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                    QuestionId = y.QuestionId
                }).ToList()
            }).ToListAsyncMethod();
        }

        /// <summary>
        /// Returns a list of question as dto by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<List<QuestionListDto>> GetUserQuestionsWithDtoAsync(int userId)
        {
            return await _questionRepository.Table.Where(que => que.CreatedBy == userId).Select(p => new QuestionListDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                CreatedDate = p.CreatedDate,
                CreatedByUserName = p.CreatedUser.UserName ?? "",
                CreatedByUserPhoto = p.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                Tags = p.Tags.Split(',', StringSplitOptions.None).ToList(),
                Comments = p.QuestionComments.Select(y => new QuestionCommentListDto
                {
                    Text = y.Text,
                    CreatedDate = y.CreatedDate,
                    Id = y.Id,
                    CreatedByUserName = y.CreatedUser.UserName ?? "",
                    CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath ?? "",
                    QuestionId = y.QuestionId
                }).ToList()
            }).ToListAsyncMethod();
        }

        #endregion
    }
}
