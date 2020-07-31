using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevPlatform.Framework.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        //public ResultModel Result;

        public BaseController() : base()
        {
            //Result = ResultModel.Success();
        }

        //protected JsonResult OkResponse<T>(T data) where T : class
        //{
        //    var response = Response<T>.Create(HttpStatusCode.OK, data);

        //    return Json(response);
        //}

        //protected JsonResult BadResponse<T>(T data) where T : class
        //{
        //    var response = Response<T>.Create(HttpStatusCode.BadRequest, data);

        //    return Json(response);
        //}
    }
}
