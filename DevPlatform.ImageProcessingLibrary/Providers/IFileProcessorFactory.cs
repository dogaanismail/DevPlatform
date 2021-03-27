using DevPlatform.ImageProcessingLibrary.Contract.Enums;
using DevPlatform.ImageProcessingLibrary.Providers.FileProcess;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    /// <summary>
    /// IFileProcessFactory interface implementations
    /// </summary>
    public interface IFileProcessorFactory
    {
        /// <summary>
        /// Creates a request
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IRequestHelper CreateRequest(PostType type);

        /// <summary>
        /// Applies a file process like edit, delete, save
        /// </summary>
        /// <returns></returns>
        IFileProcess ApplyFileProcess();
    }
}
