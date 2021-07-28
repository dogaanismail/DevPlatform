﻿using DevPlatform.Domain.Common;
using DevPlatform.Domain.Error;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace DevPlatform.Domain.Api
{
    /// <summary>
    /// Base generic response model implementations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> where T : class
    {
        public static Response<T> Create(HttpStatusCode statusCode, T result, string errorMessage = null)
        {
            return new Response<T>(statusCode, result);
        }

        public string Version => "1.0.0";

        public int StatusCode { get; set; }
        public bool Status { get; set; }

        public ValidationError ValidationError { get; set; }

        public T Result { get; set; }

        public Response()
        {
        }

        protected Response(HttpStatusCode statusCode, T result)
        {
            StatusCode = (int)statusCode;
            Status = statusCode == HttpStatusCode.OK;
            Result = result;
        }

        protected Response(T error)
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Status = false;
            Result = error;
        }

        protected Response(ResultModel resultModel)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
            Status = false;
            Result = null;
        }

        public static Response<object> ValidError(ModelStateDictionary modelState)
        {
            return new Response<object>(new ValidationError(modelState));
        }

        public static Response<object> LogicError(ResultModel resultModel)
        {
            return new Response<object>(resultModel);
        }
    }
}
