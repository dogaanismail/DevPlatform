using DevPlatform.ImageProcessingLibrary.Contract.Enums;
using DevPlatform.ImageProcessingLibrary.Providers.FileProcess;
using DevPlatform.ImageProcessingLibrary.Providers.Helper;

namespace DevPlatform.ImageProcessingLibrary.Providers.Aspose
{
    /// <summary>
    /// AsposeFactory class implementations
    /// </summary>
    public class AsposeFactory : IFileProcessorFactory
    {
        #region Methods

        /// <summary>
        /// Creates a request according to PostType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IRequestHelper CreateRequest(PostType type)
        {
            return type switch
            {
                PostType.Load => new AsposeLoadFileRequest(),
                PostType.Save => new AsposeSaveFileRequest(),
                PostType.Edit => new AsposeEditFileRequest(),
                _ => null,
            };
        }

        /// <summary>
        /// Applies a file process like edit, delete or save
        /// </summary>
        /// <returns></returns>
        public IFileProcess ApplyFileProcess()
        {
            return new AsposeProcess();
        }

        #endregion
    }
}
