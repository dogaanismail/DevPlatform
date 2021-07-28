using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace DevPlatform.Domain.Error
{
    public class ValidationError
    {
        public string Message { get; set; }

        public ValidationError(ModelStateDictionary modelState)
        {
            Message = string.Join(Environment.NewLine, modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => x.ErrorMessage)));        
        }
    }
}
