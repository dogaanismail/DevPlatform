using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Api.AlbumApi;
using DevPlatform.Domain.Common;
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
    public partial class AlbumController : BaseApiController
    {
        #region Fields
        private readonly IAlbumService _albumService;
        private readonly ILogService _logService;
        #endregion

        #region Ctor

        public AlbumController(IAlbumService albumService,
            ILogService logService)
        {
            _albumService = albumService;
            _logService = logService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Creating an album
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("createalbum")]
        [Authorize]
        public virtual JsonResult CreateAlbum([FromForm] AlbumCreateApi model)
        {
            _logService.InsertLogAsync(LogLevel.Information, $"AlbumController- Create Album Request", JsonConvert.SerializeObject(model));

            var serviceResponse = _albumService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
            {
                _logService.InsertLogAsync(LogLevel.Error, $"AlbumController- Create Album Error", JsonConvert.SerializeObject(serviceResponse));

                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = string.Join(Environment.NewLine, serviceResponse.Warnings.Select(err => string.Join(Environment.NewLine, err))),
                });
            }

            if (serviceResponse.Data.Succeeded)
                return OkResponse(Result);
            else
                return BadResponse(Result);
        }

        #endregion

        //TODO: Album list api will be added
    }
}
