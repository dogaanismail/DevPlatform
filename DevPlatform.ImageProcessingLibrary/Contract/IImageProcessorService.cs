using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;

namespace DevPlatform.ImageProcessingLibrary.Contract
{
    /// <summary>
    /// IImageProcessorService interface implementations
    /// </summary>
    public interface IImageProcessorService
    {
        /// <summary>
        /// Applies a file process like edit,save or load file
        /// </summary>
        /// <param name="imageProcessRequest"></param>
        /// <returns></returns>
        IProcessResponse Apply(ImageProcessRequest imageProcessRequest);
    }
}
