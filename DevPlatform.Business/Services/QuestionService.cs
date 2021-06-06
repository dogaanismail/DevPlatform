using DevPlatform.Business.Interfaces;
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

        #endregion

        #region Ctor

        public QuestionService(IRepository<Question> questionRepository,
            IUserService userService,
            ILogService logService,
            IHttpContextAccessor httpContextAccessor)
        {
            _questionRepository = questionRepository;
            _userService = userService;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public virtual ResultModel Create(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            _questionRepository.Insert(question);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        ///  Inserts questions by using bulk
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        public virtual ResultModel Create(List<Question> questions)
        {
            if (questions == null)
                throw new ArgumentNullException(nameof(questions));

            _questionRepository.Insert(questions);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Deletes q question
        /// </summary>
        /// <param name="question"></param>
        public virtual void Delete(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            _questionRepository.Delete(question);
        }

        /// <summary>
        /// Updates a question
        /// </summary>
        /// <param name="question"></param>
        public virtual void Update(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            _questionRepository.Update(question);
        }

        /// <summary>
        /// Gets a question by id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public virtual Question GetById(int questionId)
        {
            if (questionId == 0)
                return null;

            return _questionRepository.GetById(questionId);
        }

        /// <summary>
        /// Returns a question by id as dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual QuestionListDto GetByIdAsDto(int id)
        {
            Question getQuestion = _questionRepository.Table
              .LoadWith(x => x.QuestionComments).ThenLoad(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
              .LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail).FirstOrDefault(y => y.Id == id);

            QuestionListDto questionListDto = new QuestionListDto
            {
                Id = getQuestion.Id,
                Title = getQuestion.Title,
                Description = getQuestion.Description,
                Tags = getQuestion.Tags,
                CreatedDate = getQuestion.CreatedDate,
                CreatedByUserName = getQuestion.CreatedUser == null ? "" : getQuestion.CreatedUser.UserName,
                CreatedByUserPhoto = getQuestion.CreatedUser.UserDetail.ProfilePhotoPath,
                Comments = getQuestion.QuestionComments.ToList().Select(y => new QuestionCommentListDto
                {
                    Text = y.Text,
                    CreatedDate = y.CreatedDate,
                    Id = y.Id,
                    CreatedByUserName = y.CreatedUser.UserName,
                    CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                    QuestionId = y.QuestionId
                }).ToList()
            };

            return questionListDto;
        }

        /// <summary>
        /// Returns a list of questions as dto
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<QuestionListDto> GetQuestionList()
        {
            IEnumerable<QuestionListDto> data = _questionRepository.Table
                .LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
                .LoadWith(x => x.QuestionComments).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
                .Select(p => new QuestionListDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Tags = p.Tags,
                    CreatedDate = p.CreatedDate,
                    CreatedByUserName = p.CreatedUser == null ? "" : p.CreatedUser.UserName,
                    CreatedByUserPhoto = p.CreatedUser == null ? "" : p.CreatedUser.UserDetail.ProfilePhotoPath,
                    Comments = p.QuestionComments.ToList().Select(y => new QuestionCommentListDto
                    {
                        Text = y.Text,
                        CreatedDate = y.CreatedDate,
                        Id = y.Id,
                        CreatedByUserName = y.CreatedUser.UserName,
                        CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                        QuestionId = y.QuestionId
                    }).ToList()
                }).OrderByDescending(sa => sa.CreatedDate).AsEnumerable();

            return data;
        }

        /// <summary>
        /// Returns a list of question by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual IEnumerable<Question> GetUserQuestionsByUserId(int userId)
        {
            IEnumerable<Question> getQuestion = _questionRepository.Table
              .LoadWith(x => x.QuestionComments).ThenLoad(x => x.CreatedUser)
              .ThenLoad(x => x.UserDetail).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
              .Where(y => y.CreatedBy == userId).ToList();

            return getQuestion;
        }

        /// <summary>
        /// Returns a list of question as dto by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual IEnumerable<QuestionListDto> GetUserQuestionsWithDto(int userId)
        {
            IEnumerable<QuestionListDto> getQuestions = _questionRepository.Table.
             LoadWith(x => x.QuestionComments).ThenLoad(x => x.CreatedUser)
             .ThenLoad(x => x.UserDetail).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
             .Where(y => y.CreatedBy == userId)
              .Select(p => new QuestionListDto
              {
                  Id = p.Id,
                  Title = p.Title,
                  Description = p.Description,
                  Tags = p.Tags,
                  CreatedDate = p.CreatedDate,
                  CreatedByUserName = p.CreatedUser == null ? "" : p.CreatedUser.UserName,
                  CreatedByUserPhoto = p.CreatedUser == null ? "" : p.CreatedUser.UserDetail.ProfilePhotoPath,
                  Comments = p.QuestionComments.ToList().Select(y => new QuestionCommentListDto
                  {
                      Text = y.Text,
                      CreatedDate = y.CreatedDate,
                      Id = y.Id,
                      CreatedByUserName = y.CreatedUser.UserName,
                      CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                      QuestionId = y.QuestionId
                  }).ToList()
              }).OrderByDescending(sa => sa.CreatedDate).AsEnumerable();

            return getQuestions;
        }

        /// <summary>
        /// Inserts questions and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResponse<CreateResponse> Create(QuestionCreateApi model)
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

                var appUser = _userService.FindByUserName(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (appUser == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "User not found!" });

                #region Question crud

                var newQuestion = new Question
                {
                    Title = model.Title,
                    Description = model.Description,
                    Tags = model.Tags,
                    CreatedBy = appUser.Id
                };

                ResultModel questionModel = Create(newQuestion);

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
                    Tags = newQuestion.Tags,
                    CreatedByUserName = appUser?.UserName,
                    CreatedByUserPhoto = appUser?.UserDetail.ProfilePhotoPath,
                    CreatedDate = newQuestion.CreatedDate,
                    Comments = null
                };

                return serviceResponse;

            }
            catch (Exception ex)
            {
                _logService.InsertLogAsync(LogLevel.Error, $"QuestionService- Create Error: model {JsonConvert.SerializeObject(model)}", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.ResultCode = ResultCode.Exception;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        #endregion
    }
}
