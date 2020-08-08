﻿using DevPlatform.Domain.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevPlatform.Domain.Error
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(Response<ValidationError>.ValidError(modelState))
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }
}
