using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;

namespace DevPlatform.ImageProcessingLibrary.Providers.Helper
{
    /// <summary>
    /// IRequestHelper interface implementations
    /// </summary>
    public interface IRequestHelper
    {
        /// <summary>
        /// Creates a file process request
        /// </summary>
        /// <param name="imageProcessRequest"></param>
        /// <returns></returns>
        ImageProcessResponse CreateFileProcessRequest(ImageProcessRequest imageProcessRequest);
    }
}
