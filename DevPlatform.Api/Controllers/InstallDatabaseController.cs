using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Common;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public partial class InstallDatabaseController : BaseApiController
    {
        #region Fields
        private readonly IDatabaseService _databaseService;
        #endregion

        #region Ctor

        public InstallDatabaseController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        #endregion

        #region Methods

        [HttpGet("install")]
        [AllowAnonymous]
        public virtual JsonResult InstallDb()
        {
            var serviceResponse = _databaseService.InstallDatabase();

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = string.Join(Environment.NewLine, serviceResponse.Warnings.Select(err => string.Join(Environment.NewLine, err))),
                });

            if (serviceResponse.Data.Succeeded)
                return OkResponse(serviceResponse);
            else
                return BadResponse(serviceResponse);
        }

        #endregion
    }
}
