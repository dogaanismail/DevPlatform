using System.Collections.Generic;
using System.Linq;

namespace DevPlatform.Domain.Common
{
    public class ServiceResponse
    {
        public ServiceResponse()
        {
            Warnings = new List<string>();
        }
        public List<string> Warnings { get; set; }
        public bool Success { get; set; }
        public ResultCode ResultCode { get; set; }
    }
    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }
    }
    public class ServiceResponseList<T> : ServiceResponse
    {
        public ServiceResponseList()
        {
            Data = new List<T>();
        }
        public bool Any => Data != null && Data.Any();
        public IList<T> Data { get; set; }
    }

    public class ServiceExecute
    {
        public ServiceResponse ServiceResponse(List<string> warnings)
        {
            var sr = new ServiceResult(warnings ?? new List<string>());
            return sr.ExecuteResult();
        }
        public ServiceResponse<T> ServiceResponse<T>(T data, List<string> warnings)
        {
            var sr = new ServiceResult<T>(data, warnings ?? new List<string>());
            return sr.ExecuteResult();
        }
        public ServiceResponseList<T> ServiceResponseList<T>(IList<T> data, List<string> warnings)
        {
            var sr = new ServiceResultList<T>(data, warnings ?? new List<string>());
            return sr.ExecuteResult();
        }

        public class ServiceResult
        {

            private readonly ServiceResponse _generic;

            public ServiceResult(List<string> warnings)
            {
                _generic = new ServiceResponse
                {
                    Warnings = warnings,
                    ResultCode = !warnings.Any() ? ResultCode.Success : ResultCode.Warning,
                    Success = !warnings.Any()
                };
            }

            public ServiceResponse ExecuteResult()
            {
                return _generic;
            }
        }
        public class ServiceResult<T>
        {
            private readonly ServiceResponse<T> _generic;

            public ServiceResult(T data, List<string> warnings)
            {
                _generic = new ServiceResponse<T>
                {
                    Data = data,
                    Warnings = warnings,
                    ResultCode = !warnings.Any() ? ResultCode.Success : ResultCode.Warning,
                    Success = !warnings.Any()
                };
            }

            public ServiceResponse<T> ExecuteResult()
            {
                return _generic;
            }
        }
        public class ServiceResultList<T>
        {

            private readonly ServiceResponseList<T> _generic;

            public ServiceResultList(IList<T> data, List<string> warnings)
            {
                data = data.ToList();
                _generic = new ServiceResponseList<T>
                {
                    Data = data,
                    Warnings = warnings,
                    ResultCode = !warnings.Any() ? ResultCode.Success : ResultCode.Warning,
                    Success = !warnings.Any()
                };
            }
            public ServiceResponseList<T> ExecuteResult()
            {
                return _generic;
            }
        }
    }

    #region ResultCode Enums
    public enum ResultCode
    {

        Exception = 0,

        Success = 1,

        ValidationError = 2,

        AuthorizationError = 3,

        NoContent = 4,

        Warning = 5
    }

    #endregion
}
