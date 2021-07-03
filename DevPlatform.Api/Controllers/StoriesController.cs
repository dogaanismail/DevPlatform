using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Api.StoryApi;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.StoryDto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public class StoriesController : BaseApiController
    {
        #region Fields
        private readonly IStoryService _storyService;
        private readonly ILogService _logService;
        #endregion

        #region Ctor

        public StoriesController(IStoryService storyService,
            ILogService logService)
        {
            _storyService = storyService;
            _logService = logService;
        }

        #endregion

        #region Methods

        [HttpPost("createstory")]
        [Authorize]
        public virtual JsonResult CreateStory([FromForm] StoryCreateApi model)
        {
            _logService.InsertLogAsync(LogLevel.Information, $"StoriesController- Create Story Request", JsonConvert.SerializeObject(model));

            var serviceResponse = _storyService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
            {
                _logService.InsertLogAsync(LogLevel.Error, $"StoriesController- Create Story Error", JsonConvert.SerializeObject(serviceResponse));

                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = string.Join(Environment.NewLine, serviceResponse.Warnings.Select(err => string.Join(Environment.NewLine, err))),
                });
            }

            return OkResponse(new StoryListDto
            {
                Id = serviceResponse.Data.Id,
                Title = serviceResponse.Data?.Title,
                Description = serviceResponse.Data?.Description,
                ImageUrl = serviceResponse.Data.ImageUrl?.ToString(),
                CreatedByUserName = serviceResponse.Data?.CreatedByUserName,
                CreatedByUserPhoto = serviceResponse.Data?.CreatedByUserPhoto,
                CreatedDate = serviceResponse.Data.CreatedDate,
                VideoUrl = serviceResponse.Data?.VideoUrl,
                StoryType = serviceResponse.Data?.StoryType,
                Comments = new List<StoryCommentListDto>()
            }); ;
        }

        [HttpGet("storylist")]
        [AllowAnonymous]
        public virtual JsonResult GetStoryList()
        {
            var data = _storyService.GetStoryList();
            return OkResponse(data);
        }

        [HttpGet("id/{id}")]
        [AllowAnonymous]
        public virtual JsonResult GetById(int id)
        {
            var data = _storyService.GetByIdAsDto(id);

            return OkResponse(data);
        }

        #endregion
    }
}
