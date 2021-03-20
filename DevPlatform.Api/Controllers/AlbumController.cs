using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Api.AlbumApi;
using DevPlatform.Domain.Common;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public partial class AlbumController : BaseApiController
    {
        #region Fields
        private readonly IAlbumService _albumService;
        #endregion

        #region Ctor

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Creating album
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("createalbum")]
        [Authorize]
        public virtual JsonResult CreateAlbum([FromForm] AlbumCreateApi model)
        {
            var serviceResponse = _albumService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = serviceResponse.Warnings.First()
                });

            if (serviceResponse.Data.Succeeded)
                return OkResponse(Result);
            else
                return BadResponse(Result);
        }

        #endregion

        //TODO: Album list api will be added
    }
}
