using DevPlatform.ImageProcessingLibrary.Contract.Request;
using DevPlatform.ImageProcessingLibrary.Contract.Response.Base;

namespace DevPlatform.ImageProcessingLibrary.Providers
{
    /// <summary>
    /// IFileProcessorClient interface implementations
    /// </summary>
    public interface IFileProcessorClient
    {
        /// <summary>
        /// Loads the process
        /// </summary>
        /// <param name="imageProcessRequest"></param>
        void Load(ImageProcessRequest imageProcessRequest);

        /// <summary>
        /// Applies file process like delete,edit, save
        /// </summary>
        /// <returns></returns>
        ImageProcessResponse ApplyFileProcess();
    }
}
