using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevPlatform.Framework.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseApiController : Controller
    {
        #region Fields
        public ResultModel Result;

        #endregion

        #region Ctor
        public BaseApiController()
        {
            Result = ResultModel.Success();
        }
        #endregion

        #region Methods
        protected JsonResult OkResponse<T>(T data) where T : class
        {
            var response = Response<T>.Create(HttpStatusCode.OK, data);

            return Json(response);
        }

        protected JsonResult BadResponse<T>(T data) where T : class
        {
            var response = Response<T>.Create(HttpStatusCode.BadRequest, data);

            return Json(response);
        }

        #endregion
    }
}
