using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Api.QuestionApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.QuestionDto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public class QuestionsController : BaseApiController
    {
        #region Fields
        private readonly IQuestionService _questionService;
        private readonly IQuestionCommentService _questionCommentService;
        private readonly ILogService _logService;
        #endregion

        #region Ctor
        public QuestionsController(IQuestionService questionService,
            IQuestionCommentService questionCommentService,
            ILogService logService)
        {
            _questionService = questionService;
            _questionCommentService = questionCommentService;
            _logService = logService;
        }

        #endregion

        #region Methods

        [HttpPost("createquestion")]
        [Authorize]
        public virtual JsonResult CreateQuestion([FromBody] QuestionCreateApi model)
        {
            _logService.InsertLogAsync(LogLevel.Information, $"QuestionsController- Create Question Request", JsonConvert.SerializeObject(model));

            var serviceResponse = _questionService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
            {
                _logService.InsertLogAsync(LogLevel.Error, $"QuestionsController- Create Question Error", JsonConvert.SerializeObject(serviceResponse));

                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = string.Join(Environment.NewLine, serviceResponse.Warnings.Select(err => string.Join(Environment.NewLine, err)))
                });
            }

            return OkResponse(new QuestionListDto
            {
                Id = serviceResponse.Data.Id,
                Title = serviceResponse.Data?.Title,
                Description = serviceResponse.Data?.Description,
                //Tags = serviceResponse.Data?.Tags,
                CreatedByUserName = serviceResponse.Data?.CreatedByUserName,
                CreatedByUserPhoto = serviceResponse.Data?.CreatedByUserPhoto,
                CreatedDate = serviceResponse.Data.CreatedDate,
                Comments = null
            });
        }

        [HttpGet("questionlist")]
        [AllowAnonymous]
        public virtual JsonResult GetQuestionList()
        {
            var data = _questionService.GetQuestionList();

            return OkResponse(data);
        }

        [HttpGet("id/{id}")]
        [AllowAnonymous]
        public virtual JsonResult GetById(int id)
        {
            var data = _questionService.GetByIdAsDto(id);

            return OkResponse(data);
        }

        [HttpPost("createcomment")]
        [Authorize]
        public virtual JsonResult CreateQuestionComment([FromBody] QuestionCommentCreateApi model)
        {
            var serviceResponse = _questionCommentService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
            {
                _logService.InsertLogAsync(LogLevel.Error, $"QuestionsController- Create Question Comment Error", JsonConvert.SerializeObject(serviceResponse));

                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = string.Join(Environment.NewLine, serviceResponse.Warnings.Select(err => string.Join(Environment.NewLine, err))),
                });
            }

            return OkResponse(new QuestionCommentListDto
            {
                Text = serviceResponse.Data.Text,
                Id = serviceResponse.Data.Id,
                QuestionId = serviceResponse.Data.QuestionId,
                CreatedDate = serviceResponse.Data.CreatedDate,
                CreatedByUserName = serviceResponse.Data.CreatedByUserName,
                CreatedByUserPhoto = serviceResponse.Data.CreatedByUserPhoto
            });
        }

        #endregion
    }
}
