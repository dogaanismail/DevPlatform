using DevPlatform.ImageProcessingLibrary.Contract.Enums;
using DevPlatform.ImageProcessingLibrary.Providers.FileProcess;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers.ImageSharp
{
    /// <summary>
    /// ImageSharpFactory class implementations
    /// </summary>
    public class ImageSharpFactory : IFileProcessorFactory
    {
        #region Methods

        /// <summary>
        /// Applies a file process like edit, delete or save
        /// </summary>
        /// <returns></returns>
        public IFileProcess ApplyFileProcess()
        {
            return new ImageSharpProcess();
        }

        /// <summary>
        /// Creates a request according to PostType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IRequestHelper CreateRequest(PostType type)
        {
            return type switch
            {
                PostType.Load => new ImageSharpLoadFileRequest(),
                PostType.Save => new ImageSharpSaveFileRequest(),
                PostType.Edit => new ImageSharpEditFileRequest(),
                _ => null,
            };
        }

        #endregion
    }
}
