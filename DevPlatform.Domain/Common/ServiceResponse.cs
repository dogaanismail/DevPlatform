namespace DevPlatform.Domain.Common
{
    public class ServiceResponse
    {
        public static ServiceResponse Success()
        {
            return new ServiceResponse();
        }

        public static ServiceResponse Error(string message)
        {
            return new ServiceResponse(false, message);
        }

        public bool Status { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }

        public ServiceResponse(bool status = true, string message = "")
        {
            this.Status = status;
            this.Message = message;
        }
    }

    public class ServiceResponse<T> : ResultModel
    {
        public new static ServiceResponse<T> Success()
        {
            return new ServiceResponse<T>();
        }

        public static ServiceResponse<T> Error()
        {
            return new ServiceResponse<T>(false);
        }

        public ServiceResponse(string errorMessage)
        {
            Message = errorMessage;
            Status = false;
        }

        public ServiceResponse(T value)
        {
            this.Data = value;
        }

        public ServiceResponse(bool status = true, string message = "")
        {
            Status = status;
            Message = message;
        }

        public T Data { get; set; }
    }
}
