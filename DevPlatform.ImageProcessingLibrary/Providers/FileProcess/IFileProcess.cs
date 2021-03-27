using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers.FileProcess
{
    /// <summary>
    /// IFileResponse interface implementations
    /// </summary>
    public interface IFileProcess
    {
        /// <summary>
        /// Applies a file process like edit, save or load file
        /// </summary>
        /// <param name="imageProcessRequest"></param>
        /// <returns></returns>
        ImageProcessResponse Apply(FileProcessorRequest imageProcessRequest);
    }
}
